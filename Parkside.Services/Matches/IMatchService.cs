﻿using Parkside.Models.ViewModels;

namespace Parkside.Services.Matches
{
    public interface IMatchService
    {
        Task<MatchViewModel> GetMatch(int id);
        PagingViewModel<MatchViewModel> GetMatches(
            string? nameSearch, string? columnToSort, int pageNumber, int pageSize);
        IQueryable<MatchViewModel> GetHomePageMatches();
        Task AddMatch(int enemyTeamId, int championshipId, MatchCreateViewModel model);
        Task DeleteMatch(int id);
        Task VirtualDeleteMatch(int id);
        Task UpdateMatch(int matchId, int enemyTeamId, int championshipId, MatchUpdateViewModel model);

    }
}
