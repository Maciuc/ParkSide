using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.PlayerTrofees;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerTrofeeController : ControllerBase
    {
        private readonly IPlayerTrofeeService _playerTrofeeService;
        private readonly ILogger<PlayerTrofeeController> _logger;

        public PlayerTrofeeController(IPlayerTrofeeService playerTrofeeService, ILogger<PlayerTrofeeController> logger)
        {
            _playerTrofeeService = playerTrofeeService;
            _logger = logger;
        }

        [HttpGet("getPlayerTrofee/{playerTrofeeId}")]
        public async Task<IActionResult> GetPlayerTrofee(int playerTrofeeId)
        {
            var playerTrofee = await _playerTrofeeService.GetPlayerTrofee(playerTrofeeId);
            return Ok(playerTrofee);
        }

        [HttpGet("getPlayerTrofeees")]
        public IActionResult GetPlayerTrofees(string? NameSearch, string? OrderBy, string? PlayerTrofeeDate,
            int PageNumber = 1, int PageSize = 10)
        {
            var playerTrofees = _playerTrofeeService.GetPlayerTrofeees(NameSearch, OrderBy, PlayerTrofeeDate,
                PageNumber, PageSize);
            return Ok(playerTrofees);
        }

        [HttpGet("getHomePagePlayerTrofeees")]
        public IActionResult GetHomePagePlayerTrofees()
        {
            var playerTrofees = _playerTrofeeService.GetHomePagePlayerTrofeees();
            return Ok(playerTrofees);
        }

        [HttpPost("createPlayerTrofee/{trofeeId}/{playerId}/{championshipId}")]
        public async Task<IActionResult> AddPlayerTrofee(int trofeeId, int playerId, int championshipId, PlayerTrofeeCreateViewModel playerTrofee)
        {
            await _playerTrofeeService.AddPlayerTrofee(trofeeId, playerId, championshipId, playerTrofee);
            return Ok(playerTrofee);
        }

        [HttpDelete("physicalDeletePlayerTrofee/{playerTrofeeId}")]
        public async Task<IActionResult> DeletePlayerTrofee(int playerTrofeeId)
        {
            await _playerTrofeeService.DeletePlayerTrofee(playerTrofeeId);
            return Ok("PlayerTrofee deleted successfully");
        }

        [HttpDelete("deletePlayerTrofee/{playerTrofeeId}")]
        public async Task<IActionResult> VirtualDeletePlayerTrofee(int playerTrofeeId)
        {
            await _playerTrofeeService.VirtualDeletePlayerTrofee(playerTrofeeId);
            return Ok("PlayerTrofee deleted successfully");
        }

        [HttpPut("updatePlayerTrofee/{playerTrofeeId}/{trofeeId}/{playerId}/{championshipId}")]
        public async Task<IActionResult> UpdatePlayerTrofee(int playerTrofeeId, int trofeeId, int playerId, int championshipId,
            PlayerTrofeeUpdateViewModel playerTrofee)
        {
            await _playerTrofeeService.UpdatePlayerTrofee(playerTrofeeId, trofeeId, playerId, championshipId, playerTrofee);
            return Ok(playerTrofee);
        }
    }
}
