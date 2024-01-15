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
            };

            return finalSocialMedia;
        }

        public PagingViewModel<SocialMediaViewModel> GetSocialMedias(
            string? nameSearch, string? columnToSort, int pageNumber, int pageSize)
        {
            var socialMedias = _socialMediaRepo.GetAllQuerable();

            if (!string.IsNullOrWhiteSpace(nameSearch))
            {
                nameSearch = nameSearch.Trim();
                socialMedias = socialMedias.Where(c => c.Name.Contains(nameSearch));
            }

            switch (columnToSort)
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

            var socialMediassPerPage = socialMedias.Skip(pageSize * (pageNumber - 1))
              .Take(pageSize).Select(socialMedia => new SocialMediaViewModel
              {
                  Id = socialMedia.Id,
                  Name = socialMedia.Name,
                  Link = socialMedia.Link,
              })
              .ToList();

            var paginingList = new PagingViewModel<SocialMediaViewModel>
            {
                Count = numberOfSocialMedias,
                NumberOfPages = (int)Math.Ceiling(numberOfSocialMedias / (double)pageSize),
                Items = socialMediassPerPage
            };

            return paginingList;
        }

        public async Task AddSocialMedia(SocialMediaCreateViewModel model)
        {

            var finalSocialMedia = new SocialMedia()
            {
                Name = model.Name,
                Link = model.Link,
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

            socialMedia.Name = model.Name;
            socialMedia.Link = model.Link;

            await _socialMediaRepo.Update(socialMedia);
        }
    }
}

