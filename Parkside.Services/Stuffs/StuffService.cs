using exp.NET6.Services.DBServices;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Stuffs;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;
using Parkside.Services.Stuffes;

namespace Parkside.Services.Stuffs
{
    public class StuffService : IStuffService
    {
        private readonly IStuffRepo _stuffRepo;
        private readonly IGenericService _genericService;
        public StuffService(IStuffRepo stuffRepo, IGenericService genericService)
        {
            _stuffRepo = stuffRepo;
            _genericService = genericService;
        }

        public async Task<StuffDetailsViewModel> GetStuff(int id)
        {
            var stuff = await _stuffRepo.GetAsync(id);

            if (stuff == null)
                throw new NotFoundException("Stuff not found!");

            var finalStuff = new StuffDetailsViewModel
            {
                Id = stuff.Id,
                FirstName = stuff.FirstName,
                LastName = stuff.LastName,
                Height = stuff.Height,
                Nationality = stuff.Nationality,
                Description = stuff.Description,
                BirthDate = stuff.BirthDate,
                ImageBase64 = _genericService.GetImgBase64(stuff.ImageUrl)
            };

            return finalStuff;
        }

        public PagingViewModel<StuffViewModel> GetStuffs(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize)
        {
            var stuffs = _stuffRepo.GetAllQuerable();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim();
                stuffs = stuffs.Where(c => c.FirstName.Contains(NameSearch) ||
                c.LastName.Contains(NameSearch));
            }

            switch (OrderBy)
            {

                case ("name"):
                    stuffs = stuffs.OrderBy(c => c.LastName);
                    break;

                case ("name_desc"):
                    stuffs = stuffs.OrderByDescending(c => c.LastName);
                    break;

                default:
                    stuffs = stuffs.OrderBy(c => c.LastName);
                    break;
            }

            var numberOfStuffs = stuffs.Count();

            var stuffssPerPage = stuffs.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(stuff => new StuffViewModel
              {
                  Id = stuff.Id,
                  FirstName = stuff.FirstName,
                  LastName = stuff.LastName,
                  Height = stuff.Height,
                  Nationality = stuff.Nationality,
                  Description = stuff.Description,
                  BirthDate = stuff.BirthDate.HasValue ? stuff.BirthDate.Value.ToString("dd/MM/yyyy") : null,
                  ImageBase64 = _genericService.GetImgBase64(stuff.ImageUrl)
              })
              .ToList();

            var paginingList = new PagingViewModel<StuffViewModel>
            {
                Count = numberOfStuffs,
                NumberOfPages = (int)Math.Ceiling(numberOfStuffs / (double)PageSize),
                Items = stuffssPerPage
            };

            return paginingList;
        }

        public async Task AddStuff(StuffCreateViewModel model)
        {

            var finalStuff = new Stuff()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Height = model.Height,
                Nationality = model.Nationality,
                Description = model.Description,
                BirthDate = model.BirthDate,
                ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Stuffs")
            };

            await _stuffRepo.Add(finalStuff);
        }
        public async Task DeleteStuff(int id)
        {
            var stuff = await _stuffRepo.GetAsync(id);
            if (stuff == null)
                throw new NotFoundException("Stuff not found!");

            await _stuffRepo.Delete(id);
        }

        public async Task VirtualDeleteStuff(int id)
        {
            var stuff = await _stuffRepo.GetAsync(id);
            if (stuff == null)
                throw new NotFoundException("Stuff not found!");

            stuff.IsDeleted = true;

            await _stuffRepo.Update(stuff);
        }

        public async Task UpdateStuff(int id, StuffUpdateViewModel model)
        {
            var stuff = await _stuffRepo.GetAsync(id);
            if (stuff == null)
                throw new NotFoundException("Stuff not found!");

            stuff.FirstName = model.FirstName;
            stuff.LastName = model.LastName;
            stuff.Height = model.Height;
            stuff.Description = model.Description;
            stuff.Nationality = model.Nationality;
            stuff.BirthDate = model.BirthDate;
            stuff.ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Stuffs");

            await _stuffRepo.Update(stuff);
        }

        public IQueryable<StuffBasicViewModel> GetStuffsDropDown()
        {
            var stuffs = _stuffRepo.GetAllQuerable().Select(x => new StuffBasicViewModel
            {
                Id = x.Id,
                Name = x.LastName + " " + x.FirstName,

            });

            return stuffs;
        }
    }
}

