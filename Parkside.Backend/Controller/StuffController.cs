using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Stuffes;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StuffController : ControllerBase
    {
        private readonly IStuffService _stuffService;
        private readonly ILogger<StuffController> _logger;

        public StuffController(IStuffService stuffaService, ILogger<StuffController> logger)
        {
            _stuffService = stuffaService;
            _logger = logger;
        }

        [HttpGet("getStuff/{stuffId}")]
        public async Task<IActionResult> GetStuff(int stuffId)
        {
            var stuff = await _stuffService.GetStuff(stuffId);
            return Ok(stuff);
        }

        [HttpGet("getStuffs", Name = "stuff")]
        public IActionResult GetStuffes(string? NameSearch, string? OrderBy,
            int PageNumber = 1, int PageSize = 10)
        {
            var stuffes = _stuffService.GetStuffs(NameSearch, OrderBy,
                PageNumber, PageSize);
            return Ok(stuffes);
        }

        [HttpGet("getStuffsDropDown")]
        public IActionResult GetStuffsDropDown()
        {
            var players = _stuffService.GetStuffsDropDown();
            return Ok(players);
        }

        [HttpPost("createStuff")]
        public async Task<IActionResult> AddStuff(StuffCreateViewModel stuff)
        {
            await _stuffService.AddStuff(stuff);
            return Ok(stuff);
        }

        [HttpDelete("physicalDeleteStuff/{stuffId}")]
        public async Task<IActionResult> DeleteStuff(int stuffId)
        {
            await _stuffService.DeleteStuff(stuffId);
            return Ok("Stuff deleted successfully");
        }

        [HttpDelete("deleteStuff/{stuffId}")]
        public async Task<IActionResult> VirtualDeleteStuff(int stuffId)
        {
            await _stuffService.VirtualDeleteStuff(stuffId);
            return Ok("Stuff deleted successfully");
        }

        [HttpPut("updateStuff/{stuffId}")]
        public async Task<IActionResult> UpdateStuff(int stuffId,
            StuffUpdateViewModel stuff)
        {
            await _stuffService.UpdateStuff(stuffId, stuff);
            return Ok("Stuff updated successfully");
        }
    }
}