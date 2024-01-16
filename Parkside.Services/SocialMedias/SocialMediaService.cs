using exp.NET6.Services.DBServices;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.SocialMedias;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;

namespace Parkside.Services.SocialMedias
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly ISocialMediaRepo _socialMediaRepo;
        private readonly IGenericService _genericService;
        public SocialMediaService(ISocialMediaRepo socialMediaRepo, IGenericService genericService)
        {
            _socialMediaRepo = socialMediaRepo;
            _genericService = genericService;
        }

        public async Task<SocialMediaViewModel> GetSocialMedia(int id)
        {
            var socialMedia = await _socialMediaRepo.GetAsync(id);

            if (socialMedia == null)
                throw new NotFoundException("SocialMedia not found!");

            var finalSocialMedia = new SocialMediaViewModel
            {
                Id = socialMedia.Id,
                Name = socialMedia.Name,
                Link = socialMedia.Link,
                Platform = socialMedia.Platform
            };

            return finalSocialMedia;
        }

        public PagingViewModel<SocialMediaViewModel> GetSocialMedias(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize)
        {
            var socialMedias = _socialMediaRepo.GetAllQuerable();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim();
                socialMedias = socialMedias.Where(c => c.Name.Contains(NameSearch));
            }

            switch (OrderBy)
            {

                case ("name"):
                    socialMedias = socialMedias.OrderBy(c => c.Name);
                    break;

                case ("name_desc"):
                    socialMedias = socialMedias.OrderByDescending(c => c.Name);
                    break;

                default:
                    socialMedias = socialMedias.OrderBy(c => c.Name);
                    break;
            }

            var numberOfSocialMedias = socialMedias.Count();

            var socialMediassPerPage = socialMedias.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(socialMedia => new SocialMediaViewModel
              {
                  Id = socialMedia.Id,
                  Name = socialMedia.Name,
                  Link = socialMedia.Link,
              })
              .ToList();

            var paginingList = new PagingViewModel<SocialMediaViewModel>
            {
                Count = numberOfSocialMedias,
                NumberOfPages = (int)Math.Ceiling(numberOfSocialMedias / (double)PageSize),
                Items = socialMediassPerPage
            };

            return paginingList;
        }

        public async Task AddSocialMedia(SocialMediaCreateViewModel model)
        {
            if (model.Link != null && model.Platform != null && !model.Link.ToLower().Contains(model.Platform.ToLower()))
            {
                throw new Exception("Social media link and platform don't correspond!");
            }

            var finalSocialMedia = new SocialMedia()
            {
                Name = model.Name,
                Link = model.Link,
                Platform = model.Platform
            };

            await _socialMediaRepo.Add(finalSocialMedia);
        }
        public async Task DeleteSocialMedia(int id)
        {
            var socialMedia = await _socialMediaRepo.GetAsync(id);
            if (socialMedia == null)
                throw new NotFoundException("SocialMedia not found!");

            await _socialMediaRepo.Delete(id);
        }

        public async Task VirtualDeleteSocialMedia(int id)
        {
            var socialMedia = await _socialMediaRepo.GetAsync(id);
            if (socialMedia == null)
                throw new NotFoundException("SocialMedia not found!");

            socialMedia.IsDeleted = true;

            await _socialMediaRepo.Update(socialMedia);
        }

        public async Task UpdateSocialMedia(int id, SocialMediaUpdateViewModel model)
        {
            var socialMedia = await _socialMediaRepo.GetAsync(id);

            if (socialMedia == null)
                throw new NotFoundException("SocialMedia not found!");

            if (model.Link != null && model.Platform != null && !model.Link.ToLower().Contains(model.Platform.ToLower()))
            {
                throw new Exception("Social media link and platform don't correspond!");
            }

            socialMedia.Name = model.Name;
            socialMedia.Link = model.Link;
            socialMedia.Platform = model.Platform;

            await _socialMediaRepo.Update(socialMedia);
        }

        public IQueryable<string?> GetPlatformsDropDown()
        {
            var platforms = _socialMediaRepo.GetAllQuerable().Select(x => x.Platform).Distinct();
            return platforms;
        }

        public IQueryable<SocialMediaViewModel> GetHomePageSocialMedia()
        {
            var socialMedias = _socialMediaRepo.GetAllQuerable().Select(x => new SocialMediaViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Link = x.Link,
                Platform = x.Platform
            });

            return socialMedias;
        }
    }
}

