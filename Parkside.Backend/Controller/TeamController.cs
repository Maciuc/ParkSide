using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Teams;

namespace Parkside.Backend.Controller
{
    [Route("api")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ILogger<TeamController> _logger;

        public TeamController(ITeamService teamService, ILogger<TeamController> logger)
        {
            _teamService = teamService;
            _logger = logger;
        }

        [HttpGet("getTeam/{teamId}")]
        public async Task<IActionResult> GetTeam(int teamId)
        {
            var team = await _teamService.GetTeam(teamId);
            return Ok(team);
        }

        [HttpGet("getTeams")]
        public IActionResult GetTeams(string? nameSearch, string? columnToSort,
            int pageNumber = 1, int pageSize = 10)
        {
            var teams = _teamService.GetTeams(nameSearch, columnToSort,
                pageNumber, pageSize);
            return Ok(teams);
        }

        [HttpPost("createTeam")]
        public async Task<IActionResult> AddTeam(TeamCreateViewModel team)
        {
            await _teamService.AddTeam(team);
            return Ok(team);
        }

        [HttpDelete("physicalDeleteTeam/{teamId}")]
        public async Task<IActionResult> DeleteTeam(int teamId)
        {
            await _teamService.DeleteTeam(teamId);
            return Ok("Team deleted successfully");
        }

        [HttpDelete("deleteTeam/{teamId}")]
        public async Task<IActionResult> VirtualDeleteTeam(int teamId)
        {
            await _teamService.VirtualDeleteTeam(teamId);
            return Ok("Team deleted successfully");
        }

        [HttpPut("updateTeam/{teamId}")]
        public async Task<IActionResult> UpdateTeam(int teamId,
            TeamUpdateViewModel team)
        {
            await _teamService.UpdateTeam(teamId, team);
            return Ok("Team updated successfully");
        }
    }
}
