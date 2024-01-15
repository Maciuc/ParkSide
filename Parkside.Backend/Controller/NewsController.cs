using Microsoft.AspNetCore.Mvc;
using Parkside.Models.ViewModels;
using Parkside.Services.Newss;

namespace Parkside.Backend.Controller
{
    [Route("api")]
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

        [HttpGet("getNewses", Name = "news")]
        public IActionResult GetNewss(string? nameSearch, string? columnToSort,
            int pageNumber = 1, int pageSize = 10)
        {
            var newss = _newsService.GetNewses(nameSearch, columnToSort,
                pageNumber, pageSize);
            return Ok(newss);
        }

        [HttpPost("createNews")]
        public async Task<IActionResult> AddNews(NewsCreateViewModel news)
        {
            await _newsService.AddNews(news);
            return CreatedAtRoute("news", news);
        }

        [HttpDelete("physicalDeleteNews/{newsId}")]
        public async Task<IActionResult> DeleteNews(int newsId)
        {
            await _newsService.DeleteNews(newsId);
            return Ok();
        }

        [HttpDelete("deleteNews/{newsId}")]
        public async Task<IActionResult> VirtualDeleteNews(int newsId)
        {
            await _newsService.VirtualDeleteNews(newsId);
            return Ok();
        }

        [HttpPut("updateNews/{newsId}")]
        public async Task<IActionResult> UpdateNews(int newsId,
            NewsUpdateViewModel news)
        {
            await _newsService.UpdateNews(newsId, news);
            return Ok();
        }
    }
}