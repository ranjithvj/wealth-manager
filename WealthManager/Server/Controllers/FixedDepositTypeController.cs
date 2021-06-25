using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WealthManager.Server.Services;
using WealthManager.Shared.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WealthManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FixedDepositTypeController : ControllerBase
    {
        private readonly FixedDepositTypeService _fdTypeService;

        public FixedDepositTypeController(FixedDepositTypeService fdTypeService)
        {
            _fdTypeService = fdTypeService;
        }

        // GET: api/<FixedDepositTypeController>
        [HttpGet]
        public async Task<List<FixedDepositTypeVM>> GetAll()
        {
            return await _fdTypeService.GetAllFixedDepositTypesAsync();
        }

    }
}
