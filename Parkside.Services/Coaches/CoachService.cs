using exp.NET6.Services.DBServices;
using Org.BouncyCastle.Crypto.Macs;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Coaches;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;
using Parkside.Services.Coaches;
using System.Numerics;

namespace Parkside.Services.Coachs
{
    public class CoachService : ICoachService
    {
        private readonly ICoachRepo _coachRepo;
        private readonly IGenericService _genericService;
        public CoachService(ICoachRepo coachRepo, IGenericService genericService)
        {
            _coachRepo = coachRepo;
            _genericService = genericService;
        }

        public async Task<CoachDetailsViewModel> GetCoach(int id)
        {
            var coach = await _coachRepo.GetAsync(id);

            if (coach == null)
                throw new NotFoundException("Coach not found!");

            var finalCoach = new CoachDetailsViewModel
            {
                Id = coach.Id,
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                TeamName = coach.TeamName,
                Height = coach.Height,
                Description = coach.Description,
                BirthDate = coach.BirthDate,
                ImageBase64 = _genericService.GetImgBase64(coach.ImageUrl)
            };

            return finalCoach;
        }

        public PagingViewModel<CoachViewModel> GetCoaches(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize)
        {
            var coachs = _coachRepo.GetAllQuerable();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim();
                coachs = coachs.Where(c => c.FirstName.Contains(NameSearch) ||
                c.LastName.Contains(NameSearch));
            }

            switch (OrderBy)
            {

                case ("name"):
                    coachs = coachs.OrderBy(c => c.LastName);
                    break;

                case ("name_desc"):
                    coachs = coachs.OrderByDescending(c => c.LastName);
                    break;

                default:
                    coachs = coachs.OrderBy(c => c.LastName);
                    break;
            }

            var numberOfCoachs = coachs.Count();

            var coachssPerPage = coachs.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(coach => new CoachViewModel
              {
                  Id = coach.Id,
                  FirstName = coach.FirstName,
                  LastName = coach.LastName,
                  TeamName = coach.TeamName,
                  Height = coach.Height,
                  Description = coach.Description,
                  BirthDate = coach.BirthDate.HasValue ? coach.BirthDate.Value.ToString("dd/MM/yyyy") : null,
                  ImageBase64 = _genericService.GetImgBase64(coach.ImageUrl)
              })
              .ToList();

            var paginingList = new PagingViewModel<CoachViewModel>
            {
                Count = numberOfCoachs,
                NumberOfPages = (int)Math.Ceiling(numberOfCoachs / (double)PageSize),
                Items = coachssPerPage
            };

            return paginingList;
        }

        public async Task AddCoach(CoachCreateViewModel model)
        {

            var finalCoach = new Coach()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                TeamName = model.TeamName,
                Height = model.Height,
                Description = model.Description,
                BirthDate = model.BirthDate,
                ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Coachs")
            };

            await _coachRepo.Add(finalCoach);
        }
        public async Task DeleteCoach(int id)
        {
            var coach = await _coachRepo.GetAsync(id);
            if (coach == null)
                throw new NotFoundException("Coach not found!");

            await _coachRepo.Delete(id);
        }

        public async Task VirtualDeleteCoach(int id)
        {
            var coach = await _coachRepo.GetAsync(id);
            if (coach == null)
                throw new NotFoundException("Coach not found!");

            coach.IsDeleted = true;

            await _coachRepo.Update(coach);
        }

        public async Task UpdateCoach(int id, CoachUpdateViewModel model)
        {
            var coach = await _coachRepo.GetAsync(id);
            if (coach == null)
                throw new NotFoundException("Coach not found!");

            coach.FirstName = model.FirstName;
            coach.LastName = model.LastName;
            coach.Height = model.Height;
            coach.TeamName = model.TeamName;
            coach.Description = model.Description;
            coach.BirthDate = model.BirthDate;
            coach.ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Coachs");

            await _coachRepo.Update(coach);
        }
    }
}

