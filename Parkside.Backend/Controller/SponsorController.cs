using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Sponsors;

namespace Parkside.Backend.Controller
{
    [Route("api")]
    [ApiController]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _sponsorService;
        private readonly ILogger<SponsorController> _logger;

        public SponsorController(ISponsorService sponsorService, ILogger<SponsorController> logger)
        {
            _sponsorService = sponsorService;
            _logger = logger;
        }

        [HttpGet("getSponsor/{sponsorId}")]
        public async Task<IActionResult> GetSponsor(int sponsorId)
        {
            var category = await _sponsorService.GetSponsor(sponsorId);
            return Ok(category);
        }

        [HttpGet("getSponsors", Name = "sponsors")]
        public IActionResult GetSponsors(string? nameSearch, string? columnToSort,
            int pageNumber = 1, int pageSize = 10)
        {
            var sponsors = _sponsorService.GetSponsors(nameSearch, columnToSort, 
                pageNumber, pageSize);
            return Ok(sponsors);
        }

        [HttpPost("createSponsor")]
        public async Task<IActionResult> AddSponsor(SponsorCreateViewModel sponsor)
        {
            await _sponsorService.AddSponsor(sponsor);
            return CreatedAtRoute("sponsors", sponsor);
        }

        [HttpDelete("physicalDeleteSponsor/{sponsorId}")]
        public async Task<IActionResult> DeleteSponsor(int sponsorId)
        {
            await _sponsorService.DeleteSponsor(sponsorId);
            return Ok();
        }

        [HttpDelete("deleteSponsor/{sponsorId}")]
        public async Task<IActionResult> VirtualDeleteSponsor(int sponsorId)
        {
            await _sponsorService.VirtualDeleteSponsor(sponsorId);
            return Ok();
        }

        [HttpPut("updateSponsor/{sponsorId}")]
        public async Task<IActionResult> UpdateSponsor(int sponsorId,
            SponsorUpdateViewModel sponsor)
        {
            await _sponsorService.UpdateSponsor(sponsorId, sponsor);
            return Ok();
        }
    }
}
