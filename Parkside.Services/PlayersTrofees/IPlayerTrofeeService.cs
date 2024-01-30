using Parkside.Models.ViewModels;

namespace Parkside.Services.PlayerTrofees
{
    public interface IPlayerTrofeeService
    {
        //Task<PlayerTrofeeDetailsViewModel> GetPlayerTrofee(int id);
        PagingViewModel<PlayerTrofeeViewModel> GetPlayerTrofees(
            string? NameSearch, string? OrderBy, string? Year, int PageNumber, int PageSize);
        IQueryable<PlayerTrofeeHomeViewModel> GetHomePagePlayerTrofees(int playerId);
        Task AddPlayerTrofee(int playerHistoryId, int trofeeId);
        Task DeletePlayerTrofee(int id);
        Task VirtualDeletePlayerTrofee(int id);

    }
}
