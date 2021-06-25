using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class MutualFundController : ControllerBase
    {
        private readonly MutualFundService _mfService;

        public MutualFundController(MutualFundService mfService)
        {
            _mfService = mfService;
        }

        // GET: api/<MutualFundController>
        [HttpGet]
        public async Task<List<MutualFundVM>> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _mfService.GetAllMutualFundsAsync(userId);
        }

        // GET api/<MutualFundController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MutualFundVM>> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mf = await _mfService.GetMutualFundAsync(id.Value);

            if (mf == null)
            {
                return NotFound();
            }

            return mf;
        }

        // POST api/<MutualFundController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IEnumerable<IFormFile> files, [FromForm] string password)
        {
            try
            {
                var upload = files.FirstOrDefault();

                if (upload == null || string.IsNullOrEmpty(password))
                {
                    return BadRequest(ModelState);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _mfService.ProcessCASreportAsync(upload, password, userId);

                return NoContent();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
