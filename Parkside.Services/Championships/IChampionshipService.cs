using Parkside.Models.ViewModels;

namespace Parkside.Services.Championships
{
    public interface IChampionshipService
    {
        Task<ChampionshipViewModel> GetChampionship(int id);
        PagingViewModel<ChampionshipViewModel> GetChampionships(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize);
        Task AddChampionship(ChampionshipCreateViewModel model);
        Task DeleteChampionship(int id);
        Task VirtualDeleteChampionship(int id);
        Task UpdateChampionship(int id, ChampionshipUpdateViewModel model);

    }
}
