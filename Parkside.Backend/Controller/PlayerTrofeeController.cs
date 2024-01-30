using Microsoft.AspNetCore.Mvc;
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

        /*[HttpGet("getPlayerTrofee/{playerTrofeeId}")]
        public async Task<IActionResult> GetPlayerTrofee(int playerTrofeeId)
        {
            var playerTrofee = await _playerTrofeeService.GetPlayerTrofee(playerTrofeeId);
            return Ok(playerTrofee);
        }*/

        [HttpGet("getPlayerTrofees")]
        public IActionResult GetPlayerTrofees(string? NameSearch, string? OrderBy, string? Year,
            int PageNumber = 1, int PageSize = 10)
        {
            var playerTrofees = _playerTrofeeService.GetPlayerTrofees(NameSearch, OrderBy, Year,
                PageNumber, PageSize);
            return Ok(playerTrofees);
        }

        [HttpGet("getHomePagePlayerTrofees/{playerId}")]
        public IActionResult GetHomePagePlayerTrofees(int playerId)
        {
            var playerTrofees = _playerTrofeeService.GetHomePagePlayerTrofees(playerId);
            return Ok(playerTrofees);
        }

        [HttpPost("createPlayerTrofee/{playerHistoryId}/{trofeeId}")]
        public async Task<IActionResult> AddPlayerTrofee(int playerHistoryId, int trofeeId)
        {
            await _playerTrofeeService.AddPlayerTrofee(playerHistoryId, trofeeId);
            return Ok("PlayerTrofee created successfully");
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

        /*[HttpPut("updatePlayerTrofee/{playerTrofeeId}/{trofeeId}/{playerId}/{championshipId}")]
        public async Task<IActionResult> UpdatePlayerTrofee(int playerTrofeeId, int trofeeId, int playerId, int championshipId,
            PlayerTrofeeUpdateViewModel playerTrofee)
        {
            await _playerTrofeeService.UpdatePlayerTrofee(playerTrofeeId, trofeeId, playerId, championshipId, playerTrofee);
            return Ok(playerTrofee);
        }*/
    }
}
