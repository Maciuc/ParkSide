using Parkside.Models.ViewModels;

namespace Parkside.Services.SocialMedias
{
    public interface ISocialMediaService
    {
        Task<SocialMediaViewModel> GetSocialMedia(int id);
        PagingViewModel<SocialMediaViewModel> GetSocialMedias(
            string? nameSearch, string? columnToSort, int pageNumber, int pageSize);
        IQueryable<string?> GetPlatformsDropDown();
        IQueryable<SocialMediaViewModel> GetHomePageSocialMedia();
        Task AddSocialMedia(SocialMediaCreateViewModel model);
        Task DeleteSocialMedia(int id);
        Task VirtualDeleteSocialMedia(int id);
        Task UpdateSocialMedia(int id, SocialMediaUpdateViewModel model);

    }
}
