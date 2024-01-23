using Parkside.Models.ViewModels;

namespace Parkside.Services.Coaches
{
    public interface ICoachService
    {
        Task<CoachDetailsViewModel> GetCoach(int id);
        PagingViewModel<CoachViewModel> GetCoaches(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize);
        Task AddCoach(CoachCreateViewModel model);
        Task DeleteCoach(int id);
        Task VirtualDeleteCoach(int id);
        Task UpdateCoach(int id, CoachUpdateViewModel model);

    }
}
