using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Matches;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly ILogger<MatchController> _logger;

        public MatchController(IMatchService matchService, ILogger<MatchController> logger)
        {
            _matchService = matchService;
            _logger = logger;
        }

        [HttpGet("getMatch/{matchId}")]
        public async Task<IActionResult> GetMatch(int matchId)
        {
            var match = await _matchService.GetMatch(matchId);
            return Ok(match);
        }

        [HttpGet("getMatches")]
        public IActionResult GetMatchs(string? NameSearch, string? OrderBy,
            int PageNumber = 1, int PageSize = 10)
        {
            var matchs = _matchService.GetMatches(NameSearch, OrderBy,
                PageNumber, PageSize);
            return Ok(matchs);
        }

        [HttpGet("getHomePageMatches")]
        public IActionResult GetHomePageMatchs()
        {
            var matchs = _matchService.GetHomePageMatches();
            return Ok(matchs);
        }

        [HttpPost("createMatch/{enemyTeamId}/{championshipId}")]
        public async Task<IActionResult> AddMatch(int enemyTeamId, int championshipId, MatchCreateViewModel match)
        {
            await _matchService.AddMatch(enemyTeamId, championshipId, match);
            return Ok(match);
        }

        [HttpDelete("physicalDeleteMatch/{matchId}")]
        public async Task<IActionResult> DeleteMatch(int matchId)
        {
            await _matchService.DeleteMatch(matchId);
            return Ok("Match deleted successfully");
        }

        [HttpDelete("deleteMatch/{matchId}")]
        public async Task<IActionResult> VirtualDeleteMatch(int matchId)
        {
            await _matchService.VirtualDeleteMatch(matchId);
            return Ok("Match deleted successfully");
        }

        [HttpPut("updateMatch/{matchId}/{enemyTeamId}/{championshipId}")]
        public async Task<IActionResult> UpdateMatch(int matchId, int enemyTeamId, int championshipId, MatchUpdateViewModel match)
        {
            await _matchService.UpdateMatch(matchId, enemyTeamId, championshipId, match);
            return Ok(match);
        }
    }
}
