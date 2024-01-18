using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Trofees;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrofeeController : ControllerBase
    {
        private readonly ITrofeeService _trofeeService;
        private readonly ILogger<TrofeeController> _logger;

        public TrofeeController(ITrofeeService trofeeService, ILogger<TrofeeController> logger)
        {
            _trofeeService = trofeeService;
            _logger = logger;
        }

        [HttpGet("getTrofee/{trofeeId}")]
        public async Task<IActionResult> GetTrofee(int trofeeId)
        {
            var trofee = await _trofeeService.GetTrofee(trofeeId);
            return Ok(trofee);
        }

        [HttpGet("getTrofees")]
        public IActionResult GetTrofees(string? NameSearch, string? OrderBy,
            int PageNumber = 1, int PageSize = 10)
        {
            var trofees = _trofeeService.GetTrofees(NameSearch, OrderBy,
                PageNumber, PageSize);
            return Ok(trofees);
        }

        [HttpGet("getTrofeesDropDown")]
        public IActionResult GetTrofeesDropDown()
        {
            var trofees = _trofeeService.GetTrofeesDropDown();
            return Ok(trofees);
        }

        [HttpPost("createTrofee")]
        public async Task<IActionResult> AddTrofee(TrofeeCreateViewModel trofee)
        {
            await _trofeeService.AddTrofee(trofee);
            return Ok(trofee);
        }

        [HttpDelete("physicalDeleteTrofee/{trofeeId}")]
        public async Task<IActionResult> DeleteTrofee(int trofeeId)
        {
            await _trofeeService.DeleteTrofee(trofeeId);
            return Ok("Trofee deleted successfully");
        }

        [HttpDelete("deleteTrofee/{trofeeId}")]
        public async Task<IActionResult> VirtualDeleteTrofee(int trofeeId)
        {
            await _trofeeService.VirtualDeleteTrofee(trofeeId);
            return Ok("Trofee deleted successfully");
        }

        [HttpPut("updateTrofee/{trofeeId}")]
        public async Task<IActionResult> UpdateTrofee(int trofeeId,
            TrofeeUpdateViewModel trofee)
        {
            await _trofeeService.UpdateTrofee(trofeeId, trofee);
            return Ok("Trofee updated successfully");
        }
    }
}
