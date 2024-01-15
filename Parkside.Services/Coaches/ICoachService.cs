using Parkside.Models.ViewModels;

namespace Parkside.Services.Coaches
{
    public interface ICoachService
    {
        Task<CoachViewModel> GetCoach(int id);
        PagingViewModel<CoachViewModel> GetCoaches(
            string? nameSearch, string? columnToSort, int pageNumber, int pageSize);
        Task AddCoach(CoachCreateViewModel model);
        Task DeleteCoach(int id);
        Task VirtualDeleteCoach(int id);
        Task UpdateCoach(int id, CoachUpdateViewModel model);

    }
}
