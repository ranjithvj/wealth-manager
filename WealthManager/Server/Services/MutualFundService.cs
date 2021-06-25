using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WealthManager.Server.Data;
using WealthManager.Server.DTO;
using WealthManager.Server.Helpers;
using WealthManager.Server.Models;
using WealthManager.Shared.ViewModels;

namespace WealthManager.Server.Services
{
    public class MutualFundService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public MutualFundService(IWebHostEnvironment environment, IConfiguration configuration,
            ApplicationDbContext context)
        {
            _environment = environment;
            _configuration = configuration;
            _context = context;
        }


        public async Task ProcessCASreportAsync(IFormFile Upload, string Password, string userId)
        {
            //Parse CAS Report
            CasResponse casResponse = await ParseCASreportAsync(Upload, Password);

            //Delete existing MF data of the user
            await ClearExistingMfDataAsync(userId);

            //Add newly parsed data into the DB
            await AddMfDataAsync(userId, casResponse);

        }

        public async Task<List<MutualFundVM>> GetAllMutualFundsAsync(string userId)
        {
            var mfs = await _context.MutualFunds.Where(x => x.UserId == userId)
                .Include(x=>x.MutualFundDetail)
                .Include(x=>x.MutualFundTxns)
                .ToListAsync();

            var vms = new List<MutualFundVM>();

            var setLatestNavTasks = new List<Task>();

            foreach (var mf in mfs)
            {
                var vm = new MutualFundVM
                {
                    Id = mf.Id,
                    Name = mf.MutualFundDetail.Name,
                    Isin = mf.MutualFundDetail.Isin,
                    MutualFundTxns = ConvertMfTxnModelToVM(mf.MutualFundTxns),
                    UnitsHeld = mf.UnitsHeld,
                    InvestedAmount = mf.InvestedAmount,
                    CurrentNav = 0,
                    AnnualReturn = 0,
                    LastUpdatedBy = mf.LastUpdatedBy,
                    LastUpdatedOn = mf.LastUpdatedOnUtc
                };

                vms.Add(vm);
                setLatestNavTasks.Add(SetLatestNavPriceAsync(vm));
            }

            await Task.WhenAll(setLatestNavTasks);
            
            return vms;
        }

        public async Task<MutualFundVM> GetMutualFundAsync(int id)
        {
            var mf = await _context.MutualFunds
                .Include(x => x.MutualFundDetail)
                .Include(x => x.MutualFundTxns)
                .FirstOrDefaultAsync(x => x.Id == id);

            var vm = new MutualFundVM
            {
                Id = mf.Id,
                Name = mf.MutualFundDetail.Name,
                Isin = mf.MutualFundDetail.Isin,
                MutualFundTxns = ConvertMfTxnModelToVM(mf.MutualFundTxns),
                UnitsHeld = mf.UnitsHeld,
                InvestedAmount = mf.InvestedAmount,
                CurrentNav = 0,
                AnnualReturn = 0,
                LastUpdatedBy = mf.LastUpdatedBy,
                LastUpdatedOn = mf.LastUpdatedOnUtc
            };

            await SetLatestNavPriceAsync(vm);

            return vm;
        }

        

        #region Helpers
        private async Task<CasResponse> ParseCASreportAsync(IFormFile Upload, string Password)
        {
             var file = Path.Combine(_environment.ContentRootPath, "Uploads",
                $"CAS_{Upload.FileName[..3]}_{DateTime.UtcNow.ToString("0:MM.dd.yy_H.mm.ss")}");

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }

            try
            {
                CasResponse response = null;
                using (var _httpClient = new HttpClient())
                {
                    string baseUrl = _configuration.GetSection("CasReaderApi")["BaseUrl"];
                    string parsePdfResource = Constants.CasReaderApi.ParsePdfResource;
                    parsePdfResource = string.Format(parsePdfResource, file, Password);
                    string url = baseUrl + parsePdfResource;

                    var httpResponse = await _httpClient.GetAsync(url);
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<CasResponse>(responseString);
                }

                return response;
            }
            catch(Exception e)
            {
                throw new Exception("An error occured while parsing the CAS file", e);
            }
        }

        private async Task ClearExistingMfDataAsync(string userId)
        {
            var mfsToDelete = await _context.MutualFunds.Where(x => x.UserId == userId).ToListAsync();

            var mfIds = mfsToDelete.Select(x => x.Id).ToList();

            var mfTxnsToDelete = await _context.MutualFundTxns.Where(x => mfIds.Contains(x.Id)).ToListAsync();

            foreach (var mfTxn in mfTxnsToDelete)
            {
                _context.MutualFundTxns.Remove(mfTxn);
            }

            foreach (var mf in mfsToDelete)
            {
                _context.MutualFunds.Remove(mf);
            }

            await _context.SaveChangesAsync();
        }

        private async Task AddMfDataAsync(string userId, CasResponse casResponse)
        {
            var mfDetailDict = await GetAllMutualFundDetailsAsync();

            if (casResponse != null && casResponse.Folios != null)
            {
                foreach (var folio in casResponse.Folios)
                {
                    foreach (var scheme in folio.Schemes)
                    {
                        if(!mfDetailDict.ContainsKey(scheme.Isin))
                        {
                            throw new Exception("Error. Mutual Fund Details not found.");
                        }
                        //Add MF
                        var mf = new MutualFund
                        {
                            //Todo:Possible bug : Only works well for Growth funds.
                            //Need to analyze further for divident reinvestment
                            MutualFundDetailId = mfDetailDict[scheme.Isin],
                            UserId = userId,
                            FolioNo = folio.Folio,
                            UnitsHeld = scheme.Close,
                            InvestedAmount = 0, //Todo
                            AvgBuyingNav = 0, //Todo
                        };

                        //Save Mutual Fund Info
                        _context.MutualFunds.Add(mf);
                        await _context.SaveChangesAsync();
                        var mfId = mf.Id;

                        var txns = new List<MutualFundTxn>();
                        decimal totalInvestedAmt = 0;
                        decimal totalBuyingNav = 0;
                        decimal totalBuyingInstance = 0;
                        foreach (var txn in scheme.Transactions)
                        {
                            var mfTxn = new MutualFundTxn
                            {
                                MutualFundId = mfId,
                                TxnDate = txn.Date,
                                Description = txn.Description,
                                Amount = txn.Amount,
                                Units = txn.Units,
                                Nav = txn.Nav,
                                Balance = txn.Balance,
                                Type = txn.Type,
                                Dividend_rate = txn.DividendRate
                            };

                            txns.Add(mfTxn);

                            //Aggregation
                            var taxType = Constants.CasReaderApi.TaxTxnType;
                            if (txn.Type != taxType)
                            {
                                totalInvestedAmt += txn.Amount;
                            }
                            if (txn.Nav.HasValue) //Some error in the calc
                            {
                                totalBuyingNav += txn.Nav.Value;
                                totalBuyingInstance += 1;
                            }
                        }

                        //Save mutual fund transactions
                        _context.MutualFundTxns.AddRange(txns);
                        await _context.SaveChangesAsync();

                        //Save aggregate data
                        mf.InvestedAmount = totalInvestedAmt;
                        mf.AvgBuyingNav = Math.Round(totalBuyingNav / totalBuyingInstance, 2);

                        _context.Attach(mf).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        private async Task<Dictionary<string, int>> GetAllMutualFundDetailsAsync()
        {
            var details = await _context.MutualFundDetails.ToListAsync();

            return details.GroupBy(x=>x.Isin).ToDictionary(x => x.Key, y => y.FirstOrDefault().Id);
        }

        private async Task SetLatestNavPriceAsync(MutualFundVM mutualFund)
        {
            try
            {
                var response = new AmfiNavResponse();

                using (var _httpClient = new HttpClient())
                {
                    string baseUrl = _configuration.GetSection("AmfiNavReaderApi")["BaseUrl"];
                    string getLatestnNavResource = Constants.AmfiNavReaderApi.GetByIsinResource;
                    getLatestnNavResource = string.Format(getLatestnNavResource, mutualFund.Isin);
                    string url = baseUrl + getLatestnNavResource;

                    var httpResponse = await _httpClient.GetAsync(url);
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<AmfiNavResponse>(responseString);
                }

                mutualFund.CurrentNav = response.Nav;
                mutualFund.CurrentNavPriceDate = response.PriceDate;
            }
            catch(Exception e)
            {
                throw new Exception("An error occured while getting latest NAV prices", e);
            }
        }

        private List<MutualFundTxnVM> ConvertMfTxnModelToVM(ICollection<MutualFundTxn> models)
        {
            var list = new List<MutualFundTxnVM>();

            foreach (var model in models)
            {
                list.Add(new MutualFundTxnVM
                {
                    Id = model.Id,
                    MutualFundId = model.MutualFundId,
                    TxnDate = model.TxnDate,
                    Description = model.Description,
                    Amount = model.Amount,
                    Units = model.Units,
                    Nav = model.Nav,
                    Balance = model.Balance,
                    Type = model.Type,
                    Dividend_rate = model.Dividend_rate,
                    LastUpdatedOnUtc = model.LastUpdatedOnUtc,
                    LastUpdatedBy = model.LastUpdatedBy
                });
            }
            return list;
        }

        #endregion
    }
}
