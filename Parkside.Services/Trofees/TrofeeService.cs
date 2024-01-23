using exp.NET6.Services.DBServices;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Trofees;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;

namespace Parkside.Services.Trofees
{
    public class TrofeeService : ITrofeeService
    {
        private readonly ITrofeeRepo _trofeeRepo;
        private readonly IGenericService _genericService;
        public TrofeeService(ITrofeeRepo trofeeRepo, IGenericService genericService)
        {
            _trofeeRepo = trofeeRepo;
            _genericService = genericService;
        }

        public async Task<TrofeeViewModel> GetTrofee(int id)
        {
            var trofee = await _trofeeRepo.GetAsync(id);

            if (trofee == null)
                throw new NotFoundException("Trofee not found!");

            var finalTrofee = new TrofeeViewModel
            {
                Id = trofee.Id,
                Name = trofee.Name,
                ImageBase64 = _genericService.GetImgBase64(trofee.ImageUrl)
            };

            return finalTrofee;
        }

        public PagingViewModel<TrofeeViewModel> GetTrofees(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize)
        {
            var trofees = _trofeeRepo.GetAllQuerable();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim();
                trofees = trofees.Where(c => c.Name.Contains(NameSearch));
            }

            switch (OrderBy)
            {

                case ("name"):
                    trofees = trofees.OrderBy(c => c.Name);
                    break;

                case ("name_desc"):
                    trofees = trofees.OrderByDescending(c => c.Name);
                    break;

                default:
                    trofees = trofees.OrderBy(c => c.Name);
                    break;
            }

            var numberOfTrofees = trofees.Count();

            var trofeessPerPage = trofees.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(trofee => new TrofeeViewModel
              {
                  Id = trofee.Id,
                  Name = trofee.Name,
                  ImageBase64 = _genericService.GetImgBase64(trofee.ImageUrl)
              })
              .ToList();

            var paginingList = new PagingViewModel<TrofeeViewModel>
            {
                Count = numberOfTrofees,
                NumberOfPages = (int)Math.Ceiling(numberOfTrofees / (double)PageSize),
                Items = trofeessPerPage
            };

            return paginingList;
        }

        public async Task AddTrofee(TrofeeCreateViewModel model)
        {

            var finalTrofee = new Trofee()
            {
                Name = model.Name,
                ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Trofees")
            };

            await _trofeeRepo.Add(finalTrofee);
        }
        public async Task DeleteTrofee(int id)
        {
            var trofee = await _trofeeRepo.GetAsync(id);
            if (trofee == null)
                throw new NotFoundException("Trofee not found!");

            await _trofeeRepo.Delete(id);
        }

        public async Task VirtualDeleteTrofee(int id)
        {
            var trofee = await _trofeeRepo.GetAsync(id);
            if (trofee == null)
                throw new NotFoundException("Trofee not found!");

            trofee.IsDeleted = true;

            await _trofeeRepo.Update(trofee);
        }

        public async Task UpdateTrofee(int id, TrofeeUpdateViewModel model)
        {
            var trofee = await _trofeeRepo.GetAsync(id);

            if (trofee == null)
                throw new NotFoundException("Trofee not found!");

            trofee.Name = model.Name;
            trofee.ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Trofees");

            await _trofeeRepo.Update(trofee);
        }

        public IQueryable<TrofeeViewModel> GetTrofeesDropDown()
        {
            var trofees = _trofeeRepo.GetAllQuerable().Select(x => new TrofeeViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });

            return trofees;
        }
    }
}

