using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.PlayerHistory;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerHistoryController : ControllerBase
    {
        private readonly IPlayerHistoryService _playerHistoryService;
        private readonly ILogger<PlayerHistoryController> _logger;

        public PlayerHistoryController(IPlayerHistoryService playerHistoryService, ILogger<PlayerHistoryController> logger)
        {
            _playerHistoryService = playerHistoryService;
            _logger = logger;
        }

        [HttpGet("getPlayerHistory/{playerHistoryId}")]
        public async Task<IActionResult> GetPlayerHistory(int playerHistoryId)
        {
            var playerHistory = await _playerHistoryService.GetPlayerHistory(playerHistoryId);
            return Ok(playerHistory);
        }

        [HttpGet("getPlayerHistories")]
        public IActionResult GetPlayerHistorys(string? NameSearch, string? OrderBy, string? Year, string? PlayerRole, string? TeamName,
            int PageNumber = 1, int PageSize = 10)
        {
            var playerHistorys = _playerHistoryService.GetPlayerHistories(NameSearch, OrderBy, Year, PlayerRole, TeamName,
                PageNumber, PageSize);
            return Ok(playerHistorys);
        }

        [HttpGet("getHomePagePlayerHistory/{playerId}")]
        public IActionResult GetHomePagePlayerHistory(int playerId)
        {
            var playerHistories = _playerHistoryService.GetHomePagePlayerHistory(playerId);
            return Ok(playerHistories);
        }

        [HttpGet("getPlayerHistoryDropDown")]
        public IActionResult GetPlayerHistoryDropDown()
        {
            var playerHistories = _playerHistoryService.GetPlayerHistoryDropDown();
            return Ok(playerHistories);
        }

        [HttpPost("createPlayerHistory/{playerId}/{championshipId}")]
        public async Task<IActionResult> AddPlayerHistory(int playerId, int championshipId, PlayerHistoryCreateViewModel playerHistory)
        {
            await _playerHistoryService.AddPlayerHistory(playerId, championshipId, playerHistory);
            return Ok(playerHistory);
        }

        [HttpDelete("physicalDeletePlayerHistory/{playerHistoryId}")]
        public async Task<IActionResult> DeletePlayerHistory(int playerHistoryId)
        {
            await _playerHistoryService.DeletePlayerHistory(playerHistoryId);
            return Ok("PlayerHistory deleted successfully");
        }

        [HttpDelete("deletePlayerHistory/{playerHistoryId}")]
        public async Task<IActionResult> VirtualDeletePlayerHistory(int playerHistoryId)
        {
            await _playerHistoryService.VirtualDeletePlayerHistory(playerHistoryId);
            return Ok("PlayerHistory deleted successfully");
        }

        [HttpPut("updatePlayerHistory/{playerHistoryId}/{playerId}/{championshipId}")]
        public async Task<IActionResult> UpdatePlayerHistory(int playerHistoryId, int playerId, int championshipId,
            PlayerHistoryUpdateViewModel playerHistory)
        {
            await _playerHistoryService.UpdatePlayerHistory(playerHistoryId, playerId, championshipId, playerHistory);
            return Ok(playerHistory);
        }
    }
}
