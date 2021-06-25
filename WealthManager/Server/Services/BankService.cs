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
    public class BankService
    {
        private readonly ApplicationDbContext _context;
        public BankService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BankVM>> GetAllBanksAsync()
        {
            var models = await _context.Banks.ToListAsync();

            return ConvertModelListToVmList(models);
        }

        public async Task<BankVM> GetBankAsync(int id)
        {
            var model = await _context.Banks.FirstOrDefaultAsync(m => m.Id == id);

            return  ConvertModelToVM(model);
        }

        public async Task<BankVM> CreateBankAsync(BankVM bankVm)
        {
            var model = ConvertVMToModel(bankVm);

            _context.Banks.Add(model);
            await _context.SaveChangesAsync();

            return ConvertModelToVM(model);
        }

        public async Task<BankVM> UpdateBankAsync(BankVM bankVM)
        {
            var model = ConvertVMToModel(bankVM);

            _context.Attach(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return ConvertModelToVM(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task DeleteBankAsync(int id)
        {
            var model = await _context.Banks.FindAsync(id);

            if (model != null)
            {
                _context.Banks.Remove(model);
                await _context.SaveChangesAsync();
            }
        }


        #region Helpers

        private List<BankVM> ConvertModelListToVmList(List<Bank> models)
        {
            var vms = new List<BankVM>();

            foreach (var model in models)
            {
                var vm = new BankVM()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                vms.Add(vm);
            }
            return vms;
        }

        private BankVM ConvertModelToVM(Bank model)
        {
            if(model == null)
            {
                return null;
            }

            return new BankVM()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        private Bank ConvertVMToModel(BankVM vm)
        {
            return new Bank()
            {
                Id = vm.Id,
                Name = vm.Name
            };
        }

        #endregion
    }
}
