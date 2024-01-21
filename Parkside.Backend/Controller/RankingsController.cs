using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Reflection.Metadata;
using exp.NET6.Services.DBServices;
using Parkside.Services.Newss;

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

        [HttpGet("getRankings")]
        public IActionResult GetRankings()
        {
            var rankings = _genericService.GetRankings();

            return Ok(rankings);
        }

    }
}
