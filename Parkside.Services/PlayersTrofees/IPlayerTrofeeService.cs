using Parkside.Models.ViewModels;

namespace Parkside.Services.PlayerTrofees
{
    public interface IPlayerTrofeeService
    {
        Task<PlayerTrofeeDetailsViewModel> GetPlayerTrofee(int id);
        PagingViewModel<PlayerTrofeeViewModel> GetPlayerTrofeees(
            string? NameSearch, string? OrderBy, string? PlayerTrofeeDate, int PageNumber, int PageSize);
        IQueryable<PlayerTrofeeViewModel> GetHomePagePlayerTrofeees();
        Task AddPlayerTrofee(int trofeeId, int playerId, int championshipId, PlayerTrofeeCreateViewModel model);
        Task DeletePlayerTrofee(int id);
        Task VirtualDeletePlayerTrofee(int id);
        Task UpdatePlayerTrofee(int playerTrofeeId, int trofeeId, int playerId, int championshipId,
            PlayerTrofeeUpdateViewModel model);

    }
}
