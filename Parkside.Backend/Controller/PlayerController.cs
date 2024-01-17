using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Players;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(IPlayerService playeraService, ILogger<PlayerController> logger)
        {
            _playerService = playeraService;
            _logger = logger;
        }

        [HttpGet("getPlayer/{playerId}")]
        public async Task<IActionResult> GetPlayer(int playerId)
        {
            var player = await _playerService.GetPlayer(playerId);
            return Ok(player);
        }

        [HttpGet("getPlayers")]
        public IActionResult GetPlayers(string? NameSearch, string? Role, string? OrderBy,
            int PageNumber = 1, int PageSize = 10)
        {
            var players = _playerService.GetPlayers(NameSearch, Role, OrderBy,
                PageNumber, PageSize);
            return Ok(players);
        }

        [HttpGet("getHomePagePlayers")]
        public IActionResult GetHomePagePlayers()
        {
            var players = _playerService.GetHomePagePlayers();
            return Ok(players);
        }

        [HttpPost("createPlayer")]
        public async Task<IActionResult> AddPlayer(PlayerCreateViewModel player)
        {
            await _playerService.AddPlayer(player);
            return Ok(player);
        }

        [HttpDelete("physicalDeletePlayer/{playerId}")]
        public async Task<IActionResult> DeletePlayer(int playerId)
        {
            await _playerService.DeletePlayer(playerId);
            return Ok("Player deleted successfully");
        }

        [HttpDelete("deletePlayer/{playerId}")]
        public async Task<IActionResult> VirtualDeletePlayer(int playerId)
        {
            await _playerService.VirtualDeletePlayer(playerId);
            return Ok("Player deleted successfully");
        }

        [HttpPut("updatePlayer/{playerId}")]
        public async Task<IActionResult> UpdatePlayer(int playerId,
            PlayerUpdateViewModel player)
        {
            await _playerService.UpdatePlayer(playerId, player);
            return Ok("Player updated successfully");
        }
    }
}