using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.SocialMedias;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaService _socialMediaService;
        private readonly ILogger<SocialMediaController> _logger;

        public SocialMediaController(ISocialMediaService socialMediaService, ILogger<SocialMediaController> logger)
        {
            _socialMediaService = socialMediaService;
            _logger = logger;
        }

        [HttpGet("getSocialMedia/{socialMediaId}")]
        public async Task<IActionResult> GetSocialMedia(int socialMediaId)
        {
            var socialMedia = await _socialMediaService.GetSocialMedia(socialMediaId);
            return Ok(socialMedia);
        }

        [HttpGet("getSocialMedias")]
        public IActionResult GetSocialMedias(string? NameSearch, string? OrderBy,
            int PageNumber = 1, int PageSize = 10)
        {
            var socialMedias = _socialMediaService.GetSocialMedias(NameSearch, OrderBy,
                PageNumber, PageSize);
            return Ok(socialMedias);
        }

        [HttpGet("getHomePageSocialMedia")]
        public IActionResult GetHomePageSocialMedia()
        {
            var socialMedias = _socialMediaService.GetHomePageSocialMedia();
            return Ok(socialMedias);
        }

        [HttpGet("getPlatformsDropDown")]
        public IActionResult GetPlatformsDropDown()
        {
            var socialMedias = _socialMediaService.GetPlatformsDropDown();
            return Ok(socialMedias);
        }

        [HttpPost("createSocialMedia")]
        public async Task<IActionResult> AddSocialMedia(SocialMediaCreateViewModel socialMedia)
        {
            await _socialMediaService.AddSocialMedia(socialMedia);
            return Ok(socialMedia);
        }

        [HttpDelete("physicalDeleteSocialMedia/{socialMediaId}")]
        public async Task<IActionResult> DeleteSocialMedia(int socialMediaId)
        {
            await _socialMediaService.DeleteSocialMedia(socialMediaId);
            return Ok("Soacial media deleted successfully");
        }

        [HttpDelete("deleteSocialMedia/{socialMediaId}")]
        public async Task<IActionResult> VirtualDeleteSocialMedia(int socialMediaId)
        {
            await _socialMediaService.VirtualDeleteSocialMedia(socialMediaId);
            return Ok("Social media deleted successfully");
        }

        [HttpPut("updateSocialMedia/{socialMediaId}")]
        public async Task<IActionResult> UpdateSocialMedia(int socialMediaId,
            SocialMediaUpdateViewModel socialMedia)
        {
            await _socialMediaService.UpdateSocialMedia(socialMediaId, socialMedia);
            return Ok("Soacial media updated successfully");
        }
    }
}
