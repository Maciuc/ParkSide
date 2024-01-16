using Parkside.Models.ViewModels;

namespace Parkside.Services.Championships
{
    public interface IChampionshipService
    {
        Task<ChampionshipViewModel> GetChampionship(int id);
        PagingViewModel<ChampionshipViewModel> GetChampionships(
            string? nameSearch, string? columnToSort, int pageNumber, int pageSize);
        Task AddChampionship(ChampionshipCreateViewModel model);
        Task DeleteChampionship(int id);
        Task VirtualDeleteChampionship(int id);
        Task UpdateChampionship(int id, ChampionshipUpdateViewModel model);

    }
}
