using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Coaches;

namespace Parkside.Backend.Controller
{
    [Route("api")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _coachService;
        private readonly ILogger<CoachController> _logger;

        public CoachController(ICoachService coachaService, ILogger<CoachController> logger)
        {
            _coachService = coachaService;
            _logger = logger;
        }

        [HttpGet("getCoach/{coachId}")]
        public async Task<IActionResult> GetCoach(int coachId)
        {
            var coach = await _coachService.GetCoach(coachId);
            return Ok(coach);
        }

        [HttpGet("getCoaches", Name = "coach")]
        public IActionResult GetCoaches(string? nameSearch, string? columnToSort,
            int pageNumber = 1, int pageSize = 10)
        {
            var coachs = _coachService.GetCoaches(nameSearch, columnToSort,
                pageNumber, pageSize);
            return Ok(coachs);
        }

        [HttpPost("createCoach")]
        public async Task<IActionResult> AddCoach(CoachCreateViewModel coach)
        {
            await _coachService.AddCoach(coach);
            return CreatedAtRoute("coach", coach);
        }

        [HttpDelete("physicalDeleteCoach/{coachId}")]
        public async Task<IActionResult> DeleteCoach(int coachId)
        {
            await _coachService.DeleteCoach(coachId);
            return Ok();
        }

        [HttpDelete("deleteCoach/{coachId}")]
        public async Task<IActionResult> VirtualDeleteCoach(int coachId)
        {
            await _coachService.VirtualDeleteCoach(coachId);
            return Ok();
        }

        [HttpPut("updateCoach/{coachId}")]
        public async Task<IActionResult> UpdateCoach(int coachId,
            CoachUpdateViewModel coach)
        {
            await _coachService.UpdateCoach(coachId, coach);
            return Ok();
        }
    }
}