using Parkside.Models.ViewModels;

namespace Parkside.Services.Stuffes
{
    public interface IStuffService
    {
        Task<StuffDetailsViewModel> GetStuff(int id);
        PagingViewModel<StuffViewModel> GetStuffs(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize);
        IQueryable<StuffBasicViewModel> GetStuffsDropDown();
        Task AddStuff(StuffCreateViewModel model);
        Task DeleteStuff(int id);
        Task VirtualDeleteStuff(int id);
        Task UpdateStuff(int id, StuffUpdateViewModel model);

    }
}
