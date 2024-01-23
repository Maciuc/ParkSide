using Parkside.Models.ViewModels;

namespace Parkside.Services.PlayerHistory
{
    public interface IPlayerHistoryService
    {
        Task<PlayerHistoryDetailsViewModel> GetPlayerHistory(int id);
        PagingViewModel<PlayerHistoryViewModel> GetPlayerHistories(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize);
        IQueryable<PlayerHistoryChampionshipsViewModel> GetHomePagePlayerHistory(int playerId);
        IQueryable<PlayerHistoryDropDownViewModel> GetPlayerHistoryDropDown();
        Task AddPlayerHistory(int playerId, int championshipId, PlayerHistoryCreateViewModel model);
        Task DeletePlayerHistory(int id);
        Task VirtualDeletePlayerHistory(int id);
        Task UpdatePlayerHistory(int playerHistoryId, int playerId, int championshipId,
            PlayerHistoryUpdateViewModel model);

    }
}
