using Parkside.Models.ViewModels;

namespace Parkside.Services.Players
{
    public interface IPlayerService
    {
        Task<PlayerDetailsViewModel> GetPlayer(int id);
        PagingViewModel<PlayerViewModel> GetPlayers(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize);
        IQueryable<PlayerViewModel> GetHomePagePlayers();
        Task AddPlayer(PlayerCreateViewModel model);
        Task DeletePlayer(int id);
        Task VirtualDeletePlayer(int id);
        Task UpdatePlayer(int id, PlayerUpdateViewModel model);

    }
}
