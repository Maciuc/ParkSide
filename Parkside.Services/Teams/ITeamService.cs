using Parkside.Models.ViewModels;

namespace Parkside.Services.Teams
{
    public interface ITeamService
    {
        Task<TeamViewModel> GetTeam(int id);
        PagingViewModel<TeamViewModel> GetTeams(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize);
        Task AddTeam(TeamCreateViewModel model);
        Task DeleteTeam(int id);
        Task VirtualDeleteTeam(int id);
        Task UpdateTeam(int id, TeamUpdateViewModel model);

    }
}
