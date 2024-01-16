using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Championships;

namespace Parkside.Backend.Controller
{
    [Route("api")]
    [ApiController]
    public class ChampionshipController : ControllerBase
    {
        private readonly IChampionshipService _championshipService;
        private readonly ILogger<ChampionshipController> _logger;

        public ChampionshipController(IChampionshipService championshipService, ILogger<ChampionshipController> logger)
        {
            _championshipService = championshipService;
            _logger = logger;
        }

        [HttpGet("getChampionship/{championshipId}")]
        public async Task<IActionResult> GetChampionship(int championshipId)
        {
            var championship = await _championshipService.GetChampionship(championshipId);
            return Ok(championship);
        }

        [HttpGet("getChampionships")]
        public IActionResult GetChampionships(string? nameSearch, string? columnToSort,
            int pageNumber = 1, int pageSize = 10)
        {
            var championships = _championshipService.GetChampionships(nameSearch, columnToSort,
                pageNumber, pageSize);
            return Ok(championships);
        }

        [HttpPost("createChampionship")]
        public async Task<IActionResult> AddChampionship(ChampionshipCreateViewModel championship)
        {
            await _championshipService.AddChampionship(championship);
            return Ok(championship);
        }

        [HttpDelete("physicalDeleteChampionship/{championshipId}")]
        public async Task<IActionResult> DeleteChampionship(int championshipId)
        {
            await _championshipService.DeleteChampionship(championshipId);
            return Ok("Championship deleted successfully");
        }

        [HttpDelete("deleteChampionship/{championshipId}")]
        public async Task<IActionResult> VirtualDeleteChampionship(int championshipId)
        {
            await _championshipService.VirtualDeleteChampionship(championshipId);
            return Ok("Championship deleted successfully");
        }

        [HttpPut("updateChampionship/{championshipId}")]
        public async Task<IActionResult> UpdateChampionship(int championshipId,
            ChampionshipUpdateViewModel championship)
        {
            await _championshipService.UpdateChampionship(championshipId, championship);
            return Ok("Championship updated successfully");
        }
    }
}
