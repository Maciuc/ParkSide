using exp.NET6.Services.DBServices;
using Microsoft.AspNetCore.Mvc;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private readonly IGenericService _genericService;

        public RankingController(IGenericService genericService)
        {
            _genericService = genericService;
        }

        [HttpPut("updateRankings")]
        public async Task<IActionResult> UpdateRankings()
        {
            await _genericService.UpdateRankings();

            return Ok("Updated succesfully");
        }

        [HttpGet("getRankings")]
        public IActionResult GetRankings()
        {
            var rankings = _genericService.GetRankings();

            return Ok(rankings);
        }

    }
}
