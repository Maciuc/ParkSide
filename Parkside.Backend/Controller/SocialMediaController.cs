using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.SocialMedias;

namespace Parkside.Backend.Controller
{
    [Route("api")]
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
            var category = await _socialMediaService.GetSocialMedia(socialMediaId);
            return Ok(category);
        }

        [HttpGet("getSocialMedias", Name = "socialMedias")]
        public IActionResult GetSocialMedias(string? nameSearch, string? columnToSort,
            int pageNumber = 1, int pageSize = 10)
        {
            var socialMedias = _socialMediaService.GetSocialMedias(nameSearch, columnToSort,
                pageNumber, pageSize);
            return Ok(socialMedias);
        }

        [HttpPost("createSocialMedia")]
        public async Task<IActionResult> AddSocialMedia(SocialMediaCreateViewModel socialMedia)
        {
            await _socialMediaService.AddSocialMedia(socialMedia);
            return CreatedAtRoute("socialMedias", socialMedia);
        }

        [HttpDelete("physicalDeleteSocialMedia/{socialMediaId}")]
        public async Task<IActionResult> DeleteSocialMedia(int socialMediaId)
        {
            await _socialMediaService.DeleteSocialMedia(socialMediaId);
            return Ok();
        }

        [HttpDelete("deleteSocialMedia/{socialMediaId}")]
        public async Task<IActionResult> VirtualDeleteSocialMedia(int socialMediaId)
        {
            await _socialMediaService.VirtualDeleteSocialMedia(socialMediaId);
            return Ok();
        }

        [HttpPut("updateSocialMedia/{socialMediaId}")]
        public async Task<IActionResult> UpdateSocialMedia(int socialMediaId,
            SocialMediaUpdateViewModel socialMedia)
        {
            await _socialMediaService.UpdateSocialMedia(socialMediaId, socialMedia);
            return Ok();
        }
    }
}
