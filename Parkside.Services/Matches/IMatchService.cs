using Parkside.Models.ViewModels;

namespace Parkside.Services.Matches
{
    public interface IMatchService
    {
        Task<MatchDetailsViewModel> GetMatch(int id);
        PagingViewModel<MatchViewModel> GetMatches(
            string? NameSearch, string? OrderBy, string? MatchDate, int PageNumber, int PageSize);
        IQueryable<MatchViewModel> GetHomePageMatches();
        Task AddMatch(int enemyTeamId, int championshipId, MatchCreateViewModel model);
        Task DeleteMatch(int id);
        Task VirtualDeleteMatch(int id);
        Task UpdateMatch(int matchId, int enemyTeamId, int championshipId, MatchUpdateViewModel model);

    }
}
