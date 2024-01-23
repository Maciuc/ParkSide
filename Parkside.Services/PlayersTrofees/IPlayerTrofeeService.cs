using Parkside.Models.ViewModels;

namespace Parkside.Services.PlayerTrofees
{
    public interface IPlayerTrofeeService
    {
        Task<PlayerTrofeeDetailsViewModel> GetPlayerTrofee(int id);
        PagingViewModel<PlayerTrofeeViewModel> GetPlayerTrofeees(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize);
        IQueryable<PlayerTrofeeViewModel> GetHomePagePlayerTrofeees();
        Task AddPlayerTrofee(int playerHistoryId, int trofeeId);
        Task DeletePlayerTrofee(int id);
        Task VirtualDeletePlayerTrofee(int id);
        Task UpdatePlayerTrofee(int playerTrofeeId, int playerHistoryId, int trofeeId);

    }
}
