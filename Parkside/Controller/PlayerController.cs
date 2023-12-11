using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parkside.Models.ViewModels;
using Parkside.Services.Players;
using System.Threading.Tasks;

namespace Parkside.Backend.Controller
{
    [Route("api")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(IPlayerService playeraService, ILogger<PlayersController> logger)
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

        [HttpGet("getPlayers", Name = "player")]
        public IActionResult GetPlayers(string? nameSearch, string? columnToSort,
            int pageNumber = 1, int pageSize = 10)
        {
            var players = _playerService.GetPlayers(nameSearch, columnToSort,
                pageNumber, pageSize);
            return Ok(players);
        }

        [HttpPost("createPlayer")]
        public async Task<IActionResult> AddPlayer(PlayerCreateViewModel player)
        {
            await _playerService.AddPlayer(player);
            return CreatedAtRoute("player", player);
        }

        [HttpDelete("physicalDeletePlayer/{playerId}")]
        public async Task<IActionResult> DeletePlayer(int playerId)
        {
            await _playerService.DeletePlayer(playerId);
            return Ok();
        }

        [HttpDelete("deletePlayer/{playerId}")]
        public async Task<IActionResult> VirtualDeletePlayer(int playerId)
        {
            await _playerService.VirtualDeletePlayer(playerId);
            return Ok();
        }

        [HttpPut("updatePlayer/{playerId}")]
        public async Task<IActionResult> UpdatePlayer(int playerId,
            PlayerUpdateViewModel player)
        {
            await _playerService.UpdatePlayer(playerId, player);
            return Ok();
        }
    }
}