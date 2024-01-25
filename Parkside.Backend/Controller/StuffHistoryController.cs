using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.StuffHistory;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StuffHistoryController : ControllerBase
    {
        private readonly IStuffHistoryService _stuffHistoryService;
        private readonly ILogger<StuffHistoryController> _logger;

        public StuffHistoryController(IStuffHistoryService stuffHistoryService, ILogger<StuffHistoryController> logger)
        {
            _stuffHistoryService = stuffHistoryService;
            _logger = logger;
        }

        [HttpGet("getStuffHistory/{stuffHistoryId}")]
        public async Task<IActionResult> GetStuffHistory(int stuffHistoryId)
        {
            var stuffHistory = await _stuffHistoryService.GetStuffHistory(stuffHistoryId);
            return Ok(stuffHistory);
        }

        [HttpGet("getStuffHistories")]
        public IActionResult GetStuffHistorys(string? NameSearch, string? OrderBy, string? Year, string? TeamName,
            int PageNumber = 1, int PageSize = 10)
        {
            var stuffHistorys = _stuffHistoryService.GetStuffHistories(NameSearch, OrderBy, Year, TeamName,
                PageNumber, PageSize);
            return Ok(stuffHistorys);
        }

        [HttpGet("getHomePageStuffHistory/{stuffId}")]
        public IActionResult GetHomePageStuffHistory(int stuffId)
        {
            var stuffHistories = _stuffHistoryService.GetHomePageStuffHistory(stuffId);
            return Ok(stuffHistories);
        }

        [HttpGet("getStuffHistoryDropDown")]
        public IActionResult GetStuffHistoryDropDown()
        {
            var stuffHistories = _stuffHistoryService.GetStuffHistoryDropDown();
            return Ok(stuffHistories);
        }

        [HttpPost("createStuffHistory/{stuffId}/{championshipId}")]
        public async Task<IActionResult> AddStuffHistory(int stuffId, int championshipId, StuffHistoryCreateViewModel stuffHistory)
        {
            await _stuffHistoryService.AddStuffHistory(stuffId, championshipId, stuffHistory);
            return Ok(stuffHistory);
        }

        [HttpDelete("physicalDeleteStuffHistory/{stuffHistoryId}")]
        public async Task<IActionResult> DeleteStuffHistory(int stuffHistoryId)
        {
            await _stuffHistoryService.DeleteStuffHistory(stuffHistoryId);
            return Ok("StuffHistory deleted successfully");
        }

        [HttpDelete("deleteStuffHistory/{stuffHistoryId}")]
        public async Task<IActionResult> VirtualDeleteStuffHistory(int stuffHistoryId)
        {
            await _stuffHistoryService.VirtualDeleteStuffHistory(stuffHistoryId);
            return Ok("StuffHistory deleted successfully");
        }

        [HttpPut("updateStuffHistory/{stuffHistoryId}/{stuffId}/{championshipId}")]
        public async Task<IActionResult> UpdateStuffHistory(int stuffHistoryId, int stuffId, int championshipId,
            StuffHistoryUpdateViewModel stuffHistory)
        {
            await _stuffHistoryService.UpdateStuffHistory(stuffHistoryId, stuffId, championshipId, stuffHistory);
            return Ok(stuffHistory);
        }
    }
}
