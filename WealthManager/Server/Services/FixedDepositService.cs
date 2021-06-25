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
    public class FixedDepositService
    {
        private readonly ApplicationDbContext _context;
        public FixedDepositService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FixedDepositVM>> GetAllFdsAsync(string userId)
        {
            var models = await _context.FixedDeposits.Where(x=>x.UserId == userId)
                            .Include(x=>x.Bank)
                            .Include(x=>x.User)
                            .Include(x => x.FixedDepositType)
                            .ToListAsync();
            
            return ConvertModelListToVmList(models);
        }

        public async Task<FixedDepositVM> GetFdAsync(int id)
        {
            var model = await _context.FixedDeposits
                            .Include(x => x.Bank)
                            .Include(x => x.User)
                            .Include(x => x.FixedDepositType)
                            .FirstOrDefaultAsync(m => m.Id == id);

            return  ConvertModelToVM(model);
        }

        public async Task<FixedDepositVM> CreateFdAsync(FixedDepositVM fdVm)
        {
            var model = ConvertVMToModel(fdVm);

            _context.FixedDeposits.Add(model);

            await _context.SaveChangesAsync();

            await _context.Entry(model).Reference(x => x.Bank).LoadAsync();
            await _context.Entry(model).Reference(x => x.FixedDepositType).LoadAsync();

            return ConvertModelToVM(model);
        }

        public async Task<FixedDepositVM> UpdateFdAsync(FixedDepositVM fdVm)
        {
            var model = ConvertVMToModel(fdVm);

            _context.Attach(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                await _context.Entry(model).Reference(x => x.Bank).LoadAsync();
                await _context.Entry(model).Reference(x => x.FixedDepositType).LoadAsync();

                return ConvertModelToVM(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task DeleteFdAsync(int id)
        {
            var model = await _context.FixedDeposits.FindAsync(id);

            if (model != null)
            {
                _context.FixedDeposits.Remove(model);
                await _context.SaveChangesAsync();
            }
        }


        #region Helpers

        private List<FixedDepositVM> ConvertModelListToVmList(List<FixedDeposit> models)
        {
            var vms = new List<FixedDepositVM>();

            foreach (var model in models)
            {
                var vm = new FixedDepositVM()
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    BankId = model.BankId,
                    BankName = model.Bank?.Name,
                    FixedDepositTypeId = model.FixedDepositTypeId,
                    FixedDepositTypeName = model.FixedDepositType?.Name,
                    Principal = model.Principal,
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

        private FixedDepositVM ConvertModelToVM(FixedDeposit model)
        {
            if(model == null)
            {
                return null;
            }

            return new FixedDepositVM()
            {
                Id = model.Id,
                UserId = model.UserId,
                BankId = model.BankId,
                BankName = model.Bank?.Name,
                FixedDepositTypeId = model.FixedDepositTypeId,
                FixedDepositTypeName = model.FixedDepositType?.Name,
                Principal = model.Principal,
                RateOfInterest = model.RateOfInterest,
                CompoundFrequency = model.CompoundFrequency,
                StartDate = model.StartDate,
                MaturityDate = model.MaturityDate,
                Duration_Years = model.Duration_Years,
                Duration_Months = model.Duration_Months,
                MaturityAmount = model.MaturityAmount,
                LastUpdatedOn = model.LastUpdatedOnUtc,
                LastUpdatedBy = model.LastUpdatedBy,
            };
        }

        private FixedDeposit ConvertVMToModel(FixedDepositVM vm)
        {
            return new FixedDeposit()
            {
                Id = vm.Id,
                UserId = vm.UserId,
                BankId = vm.BankId,
                FixedDepositTypeId = vm.FixedDepositTypeId,
                Principal = vm.Principal,
                RateOfInterest = vm.RateOfInterest,
                CompoundFrequency = vm.CompoundFrequency,
                StartDate = vm.StartDate,
                MaturityDate = vm.MaturityDate,
                Duration_Years = vm.Duration_Years,
                Duration_Months = vm.Duration_Months,
                MaturityAmount = vm.MaturityAmount,
            };
        }

        #endregion
    }
}
