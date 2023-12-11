using exp.NET6.Services.DBServices;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Sponsors;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;

namespace Parkside.Services.Sponsors
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepo _sponsorRepo;
        private readonly IGenericService _genericService;
        public SponsorService(ISponsorRepo sponsorRepo, IGenericService genericService)
        {
            _sponsorRepo = sponsorRepo;
            _genericService = genericService;
        }

        public async Task<SponsorViewModel> GetSponsor(int id)
        {
            var sponsor = await _sponsorRepo.GetAsync(id);

            if (sponsor == null)
                throw new NotFoundException("Sponsor not found!");

            var finalSponsor = new SponsorViewModel
            {
                Id = sponsor.Id,
                Name = sponsor.Name,
                Link = sponsor.Link,
                ImageBase64 = _genericService.GetImgBase64(sponsor.ImageUrl)
            };

            return finalSponsor;
        }

        public PagingViewModel<SponsorViewModel> GetSponsors(
            string? nameSearch, string? columnToSort, int pageNumber, int pageSize)
        {
            var sponsors = _sponsorRepo.GetAllQuerable();

            if (!string.IsNullOrWhiteSpace(nameSearch))
            {
                nameSearch = nameSearch.Trim();
                sponsors = sponsors.Where(c => c.Name.Contains(nameSearch));
            }

            switch (columnToSort)
            {

                case ("name"):
                    sponsors = sponsors.OrderBy(c => c.Name);
                    break;

                case ("name_desc"):
                    sponsors = sponsors.OrderByDescending(c => c.Name);
                    break;

                default:
                    sponsors = sponsors.OrderBy(c => c.Name);
                    break;
            }

            var numberOfSponsors = sponsors.Count();

            var sponsorssPerPage = sponsors.Skip(pageSize * (pageNumber - 1))
              .Take(pageSize).Select(sponsor => new SponsorViewModel
              {
                  Id = sponsor.Id,
                  Name = sponsor.Name,
                  Link = sponsor.Link,
                  ImageBase64 = _genericService.GetImgBase64(sponsor.ImageUrl)
              })
              .ToList();

            var paginingList = new PagingViewModel<SponsorViewModel>
            {
                Count = numberOfSponsors,
                NumberOfPages = (int)Math.Ceiling(numberOfSponsors / (double)pageSize),
                Items = sponsorssPerPage
            };

            return paginingList;
        }

        public async Task AddSponsor(SponsorCreateViewModel model)
        {

            var finalSponsor = new Sponsor()
            {
                Name = model.Name,
                Link = model.Link,
                ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Sponsors")
            };

            await _sponsorRepo.Add(finalSponsor);
        }
        public async Task DeleteSponsor(int id)
        {
            var sponsor = await _sponsorRepo.GetAsync(id);
            if (sponsor == null)
                throw new NotFoundException("Sponsor not found!");

            await _sponsorRepo.Delete(id);
        }

        public async Task VirtualDeleteSponsor(int id)
        {
            var sponsor = await _sponsorRepo.GetAsync(id);
            if (sponsor == null)
                throw new NotFoundException("Sponsor not found!");

            sponsor.IsDeleted = true;

            await _sponsorRepo.Update(sponsor);
        }

        public async Task UpdateSponsor(int id, SponsorUpdateViewModel model)
        {
            var sponsor = await _sponsorRepo.GetAsync(id);
            if (sponsor == null)
                throw new NotFoundException("Sponsor not found!");

            sponsor.Name = model.Name;
            sponsor.Link = model.Link;
            sponsor.ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Sponsors");

            await _sponsorRepo.Update(sponsor);
        }
    }
}

