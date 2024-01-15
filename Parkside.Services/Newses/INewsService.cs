using Parkside.Models.ViewModels;

namespace Parkside.Services.Newss
{
    public interface INewsService
    {
        Task<NewsViewModel> GetNews(int id);
        PagingViewModel<NewsViewModel> GetNewses(
            string? nameSearch, string? columnToSort, int pageNumber, int pageSize);
        Task AddNews(NewsCreateViewModel model);
        Task DeleteNews(int id);
        Task VirtualDeleteNews(int id);
        Task UpdateNews(int id, NewsUpdateViewModel model);

    }
}
