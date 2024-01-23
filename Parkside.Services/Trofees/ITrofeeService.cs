using Parkside.Models.ViewModels;

namespace Parkside.Services.Trofees
{
    public interface ITrofeeService
    {
        Task<TrofeeViewModel> GetTrofee(int id);
        PagingViewModel<TrofeeViewModel> GetTrofees(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize);
        IQueryable<TrofeeViewModel> GetTrofeesDropDown();
        Task AddTrofee(TrofeeCreateViewModel model);
        Task DeleteTrofee(int id);
        Task VirtualDeleteTrofee(int id);
        Task UpdateTrofee(int id, TrofeeUpdateViewModel model);

    }
}
