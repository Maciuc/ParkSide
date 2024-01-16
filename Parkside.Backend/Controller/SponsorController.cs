using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Sponsors;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
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
            var sponsor = await _sponsorService.GetSponsor(sponsorId);
            return Ok(sponsor);
        }

        [HttpGet("getSponsors")]
        public IActionResult GetSponsors(string? NameSearch,
            int PageNumber = 1, int PageSize = 10)
        {
            var sponsors = _sponsorService.GetSponsors(NameSearch,
                PageNumber, PageSize);
            return Ok(sponsors);
        }

        [HttpGet("getHomePageSponsors")]
        public IActionResult GetHomePageSponsors()
        {
            var sponsors = _sponsorService.GetHomePageSponsors();
            return Ok(sponsors);
        }

        [HttpPost("createSponsor")]
        public async Task<IActionResult> AddSponsor(SponsorCreateViewModel sponsor)
        {
            await _sponsorService.AddSponsor(sponsor);
            return Ok(sponsor);
        }

        [HttpDelete("physicalDeleteSponsor/{sponsorId}")]
        public async Task<IActionResult> DeleteSponsor(int sponsorId)
        {
            await _sponsorService.DeleteSponsor(sponsorId);
            return Ok("Sponsor deleted successfully");
        }

        [HttpDelete("deleteSponsor/{sponsorId}")]
        public async Task<IActionResult> VirtualDeleteSponsor(int sponsorId)
        {
            await _sponsorService.VirtualDeleteSponsor(sponsorId);
            return Ok("Sponsor deleted successfully");
        }

        [HttpPut("updateSponsor/{sponsorId}")]
        public async Task<IActionResult> UpdateSponsor(int sponsorId,
            SponsorUpdateViewModel sponsor)
        {
            await _sponsorService.UpdateSponsor(sponsorId, sponsor);
            return Ok("Sponsor updated successfully");
        }
    }
}
