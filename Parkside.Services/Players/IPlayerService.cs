using Parkside.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Parkside.Services.Players
{
    public interface IPlayerService
    {
        Task<PlayerViewModel> GetPlayer(int id);
        PagingViewModel<PlayerViewModel> GetPlayers(
            string? nameSearch, string? columnToSort, int pageNumber, int pageSize);
        Task AddPlayer(PlayerCreateViewModel model);
        Task DeletePlayer(int id);
        Task VirtualDeletePlayer(int id);
        Task UpdatePlayer(int id, PlayerUpdateViewModel model);

    }
}
