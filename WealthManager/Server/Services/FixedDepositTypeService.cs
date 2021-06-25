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
    public class FixedDepositTypeService
    {
        private readonly ApplicationDbContext _context;
        public FixedDepositTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FixedDepositTypeVM>> GetAllFixedDepositTypesAsync()
        {
            var models = await _context.FixedDepositTypes.ToListAsync();

            return ConvertModelListToVmList(models);
        }


        #region Helpers

        private List<FixedDepositTypeVM> ConvertModelListToVmList(List<FixedDepositType> models)
        {
            var vms = new List<FixedDepositTypeVM>();

            foreach (var model in models)
            {
                var vm = new FixedDepositTypeVM()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                vms.Add(vm);
            }
            return vms;
        }

        #endregion
    }
}
