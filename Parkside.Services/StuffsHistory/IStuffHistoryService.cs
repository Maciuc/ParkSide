using Parkside.Models.ViewModels;

namespace Parkside.Services.StuffHistory
{
    public interface IStuffHistoryService
    {
        Task<StuffHistoryDetailsViewModel> GetStuffHistory(int id);
        PagingViewModel<StuffHistoryViewModel> GetStuffHistories(
            string? NameSearch, string? OrderBy, string? Year, string? TeamName,
            int PageNumber, int PageSize);
        IQueryable<StuffHistoryChampionshipsViewModel> GetHomePageStuffHistory(int stuffId);
        IQueryable<StuffHistoryDropDownViewModel> GetStuffHistoryDropDown();
        Task AddStuffHistory(int stuffId, StuffHistoryCreateViewModel model);
        Task DeleteStuffHistory(int id);
        Task VirtualDeleteStuffHistory(int id);
        Task UpdateStuffHistory(int stuffHistoryId, int stuffId,
            StuffHistoryUpdateViewModel model);

    }
}
