using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WealthManager.Server.Data;
using WealthManager.Server.Models;
using WealthManager.Shared.ViewModels;

namespace WealthManager.Server.Services
{
    public class RecurringDepositService
    {
        private readonly ApplicationDbContext _context;
        public RecurringDepositService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RecurringDepositVM>> GetAllRdsAsync(string userId)
        {
            var models = await _context.RecurringDeposits.Where(x=>x.UserId == userId)
                            .Include(x=>x.Bank)
                            .Include(x=>x.User)
                            .ToListAsync();
            
            return ConvertModelListToVmList(models);
        }

        public async Task<RecurringDepositVM> GetRdAsync(int id)
        {
            var model = await _context.RecurringDeposits
                            .Include(x => x.Bank)
                            .Include(x => x.User)
                            .FirstOrDefaultAsync(m => m.Id == id);

            return  ConvertModelToVM(model);
        }

        public async Task<RecurringDepositVM> CreateRdAsync(RecurringDepositVM rdVm)
        {
            var model = ConvertVMToModel(rdVm);

            _context.RecurringDeposits.Add(model);

            await _context.SaveChangesAsync();

            await _context.Entry(model).Reference(x => x.Bank).LoadAsync();

            return ConvertModelToVM(model);
        }

        public async Task<RecurringDepositVM> UpdateRdAsync(RecurringDepositVM rdVm)
        {
            var model = ConvertVMToModel(rdVm);

            _context.Attach(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                await _context.Entry(model).Reference(x => x.Bank).LoadAsync();

                return ConvertModelToVM(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task DeleteRdAsync(int id)
        {
            var model = await _context.RecurringDeposits.FindAsync(id);

            if (model != null)
            {
                _context.RecurringDeposits.Remove(model);
                await _context.SaveChangesAsync();
            }
        }


        #region Helpers

        private List<RecurringDepositVM> ConvertModelListToVmList(List<RecurringDeposit> models)
        {
            var vms = new List<RecurringDepositVM>();

            foreach (var model in models)
            {
                var vm = new RecurringDepositVM()
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    BankId = model.BankId,
                    BankName = model.Bank?.Name,
                    MonthlyInstallment = model.MonthlyInstallment,
                    RateOfInterest = model.RateOfInterest,
                    CompoundFrequency = model.CompoundFrequency,
                    StartDate = model.StartDate,
                    MaturityDate = model.MaturityDate,
                    Duration_Years = model.Duration_Years,
                    Duration_Months = model.Duration_Months,
                    MaturityAmount = model.MaturityAmount,
                    LastUpdatedBy = model.LastUpdatedBy,
                    LastUpdatedOn = model.LastUpdatedOnUtc
                };
                vms.Add(vm);
            }
            return vms;
        }

        private RecurringDepositVM ConvertModelToVM(RecurringDeposit model)
        {
            if(model == null)
            {
                return null;
            }

            return new RecurringDepositVM()
            {
                Id = model.Id,
                UserId = model.UserId,
                BankId = model.BankId,
                BankName = model.Bank.Name,
                MonthlyInstallment = model.MonthlyInstallment,
                RateOfInterest = model.RateOfInterest,
                CompoundFrequency = model.CompoundFrequency,
                StartDate = model.StartDate,
                MaturityDate = model.MaturityDate,
                Duration_Years = model.Duration_Years,
                Duration_Months = model.Duration_Months,
                MaturityAmount = model.MaturityAmount,
                LastUpdatedBy = model.LastUpdatedBy,
                LastUpdatedOn = model.LastUpdatedOnUtc
            };
        }

        private RecurringDeposit ConvertVMToModel(RecurringDepositVM vm)
        {
            return new RecurringDeposit()
            {
                Id = vm.Id,
                UserId = vm.UserId,
                BankId = vm.BankId,
                MonthlyInstallment = vm.MonthlyInstallment,
                RateOfInterest = vm.RateOfInterest,
                CompoundFrequency = vm.CompoundFrequency,
                StartDate = vm.StartDate,
                MaturityDate = vm.MaturityDate,
                Duration_Years = vm.Duration_Years,
                Duration_Months = vm.Duration_Months,
                MaturityAmount = vm.MaturityAmount,
                //LastUpdatedBy = DateTime.UtcNow
            };
        }

        #endregion
    }
}
