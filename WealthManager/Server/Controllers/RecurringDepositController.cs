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
    public class RecurringDepositController : ControllerBase
    {
        private readonly RecurringDepositService _rdService;

        public RecurringDepositController(RecurringDepositService rdService)
        {
            _rdService = rdService;
        }

        // GET: api/<RecurringDepositController>
        [HttpGet]
        public async Task<List<RecurringDepositVM>> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _rdService.GetAllRdsAsync(userId);
        }

        // GET api/<RecurringDepositController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecurringDepositVM>> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rd = await _rdService.GetRdAsync(id.Value);

            if (rd == null)
            {
                return NotFound();
            }

            return rd;
        }

        // POST api/<RecurringDepositController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RecurringDepositVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            vm.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _rdService.CreateRdAsync(vm);

            return NoContent();
        }

        // PUT api/<RecurringDepositController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] RecurringDepositVM vm)
        {
            if (!ModelState.IsValid || id == null || id.Value == 0)
            {
                return BadRequest(ModelState);
            }

            await _rdService.UpdateRdAsync(vm);

            return NoContent();
        }

        // DELETE api/<RecurringDepositController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _rdService.DeleteRdAsync(id.Value);

            return NoContent();
        }
    }
}
