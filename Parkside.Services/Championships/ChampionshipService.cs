using exp.NET6.Services.DBServices;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.ChampionShips;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;

namespace Parkside.Services.Championships
{
    public class ChampionshipService : IChampionshipService
    {
        private readonly IChampionshipRepo _championshipRepo;
        private readonly IGenericService _genericService;
        public ChampionshipService(IChampionshipRepo championshipRepo, IGenericService genericService)
        {
            _championshipRepo = championshipRepo;
            _genericService = genericService;
        }

        public async Task<ChampionshipViewModel> GetChampionship(int id)
        {
            var championship = await _championshipRepo.GetAsync(id);

            if (championship == null)
                throw new NotFoundException("Championship not found!");

            var finalChampionship = new ChampionshipViewModel
            {
                Id = championship.Id,
                Name = championship.Name,
                ImageBase64 = _genericService.GetImgBase64(championship.ImageUrl)
            };

            return finalChampionship;
        }

        public PagingViewModel<ChampionshipViewModel> GetChampionships(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize)
        {
            var championships = _championshipRepo.GetAllQuerable();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim();
                championships = championships.Where(c => c.Name.Contains(NameSearch));
            }

            switch (OrderBy)
            {

                case ("name"):
                    championships = championships.OrderBy(c => c.Name);
                    break;

                case ("name_desc"):
                    championships = championships.OrderByDescending(c => c.Name);
                    break;

                default:
                    championships = championships.OrderBy(c => c.Name);
                    break;
            }

            var numberOfChampionships = championships.Count();

            var championshipssPerPage = championships.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(championship => new ChampionshipViewModel
              {
                  Id = championship.Id,
                  Name = championship.Name,
                  ImageBase64 = _genericService.GetImgBase64(championship.ImageUrl)
              })
              .ToList();

            var paginingList = new PagingViewModel<ChampionshipViewModel>
            {
                Count = numberOfChampionships,
                NumberOfPages = (int)Math.Ceiling(numberOfChampionships / (double)PageSize),
                Items = championshipssPerPage
            };

            return paginingList;
        }

        public async Task AddChampionship(ChampionshipCreateViewModel model)
        {

            var finalChampionship = new Championship()
            {
                Name = model.Name,
                ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Championships")
            };

            await _championshipRepo.Add(finalChampionship);
        }
        public async Task DeleteChampionship(int id)
        {
            var championship = await _championshipRepo.GetAsync(id);
            if (championship == null)
                throw new NotFoundException("Championship not found!");

            await _championshipRepo.Delete(id);
        }

        public async Task VirtualDeleteChampionship(int id)
        {
            var championship = await _championshipRepo.GetAsync(id);
            if (championship == null)
                throw new NotFoundException("Championship not found!");

            championship.IsDeleted = true;

            await _championshipRepo.Update(championship);
        }

        public async Task UpdateChampionship(int id, ChampionshipUpdateViewModel model)
        {
            var championship = await _championshipRepo.GetAsync(id);
            if (championship == null)
                throw new NotFoundException("Championship not found!");

            championship.Name = model.Name;
            championship.ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Championships");

            await _championshipRepo.Update(championship);
        }

        public IQueryable<ChampionshipViewModel> GetChampionshipsDropDown()
        {
            var championships = _championshipRepo.GetAllQuerable().Select(x => new ChampionshipViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });

            return championships;
        }
    }
}

