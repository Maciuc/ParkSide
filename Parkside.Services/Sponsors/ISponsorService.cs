using Parkside.Models.ViewModels;

namespace Parkside.Services.Sponsors
{
    public interface ISponsorService
    {
        Task<SponsorViewModel> GetSponsor(int id);
        PagingViewModel<SponsorViewModel> GetSponsors(
            string? nameSearch, string? columnToSort, int pageNumber, int pageSize);
        Task AddSponsor(SponsorCreateViewModel model);
        Task DeleteSponsor(int id);
        Task VirtualDeleteSponsor(int id);
        Task UpdateSponsor(int id, SponsorUpdateViewModel model);

    }
}
