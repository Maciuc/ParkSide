using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Newss;

namespace Parkside.Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;

        public NewsController(INewsService newsaService, ILogger<NewsController> logger)
        {
            _newsService = newsaService;
            _logger = logger;
        }

        [HttpGet("getNews/{newsId}")]
        public async Task<IActionResult> GetNews(int newsId)
        {
            var news = await _newsService.GetNews(newsId);
            return Ok(news);
        }

        [HttpGet("getNewses")]
        public IActionResult GetNewses(string? NameSearch, string? OrderBy,
            string? PublishedDate, bool? IsPublished,
            int PageNumber = 1, int PageSize = 10)
        {
            var newses = _newsService.GetNewses(NameSearch, OrderBy, PublishedDate, IsPublished,
                PageNumber, PageSize);
            return Ok(newses);
        }

        [HttpGet("getHomePageNewses")]
        public IActionResult GetHomePageNewses()
        {
            var newses = _newsService.GetHomePageNewses();
            return Ok(newses);
        }

        [HttpGet("getLatestNewses")]
        public IActionResult GetLatestNewses()
        {
            var newses = _newsService.GetLatestNewses();
            return Ok(newses);
        }

        [HttpPost("createNews")]
        public async Task<IActionResult> AddNews(NewsCreateViewModel news)
        {
            await _newsService.AddNews(news);
            return Ok(news);
        }

        [HttpDelete("physicalDeleteNews/{newsId}")]
        public async Task<IActionResult> DeleteNews(int newsId)
        {
            await _newsService.DeleteNews(newsId);
            return Ok("News deleted successfully");
        }

        [HttpDelete("deleteNews/{newsId}")]
        public async Task<IActionResult> VirtualDeleteNews(int newsId)
        {
            await _newsService.VirtualDeleteNews(newsId);
            return Ok("News deleted successfully");
        }

        [HttpPut("updateNews/{newsId}")]
        public async Task<IActionResult> UpdateNews(int newsId,
            NewsUpdateViewModel news)
        {
            await _newsService.UpdateNews(newsId, news);
            return Ok("News updated successfully");
        }
    }
}