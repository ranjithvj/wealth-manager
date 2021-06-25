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
    public class FixedDepositController : ControllerBase
    {
        private readonly FixedDepositService _fdService;

        public FixedDepositController(FixedDepositService fdService)
        {
            _fdService = fdService;
        }

        // GET: api/<FixedDepositController>
        [HttpGet]
        public async Task<List<FixedDepositVM>> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _fdService.GetAllFdsAsync(userId);
        }

        // GET api/<FixedDepositController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FixedDepositVM>> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fd = await _fdService.GetFdAsync(id.Value);

            if (fd == null)
            {
                return NotFound();
            }

            return fd;
        }

        // POST api/<FixedDepositController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FixedDepositVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            vm.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _fdService.CreateFdAsync(vm);

            return NoContent();
        }

        // PUT api/<FixedDepositController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromBody] FixedDepositVM vm)
        {
            if (!ModelState.IsValid || id == null || id.Value == 0)
            {
                return BadRequest(ModelState);
            }

            await _fdService.UpdateFdAsync(vm);

            return NoContent();
        }

        // DELETE api/<FixedDepositController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _fdService.DeleteFdAsync(id.Value);

            return NoContent();
        }
    }
}
