using exp.NET6.Services.DBServices;
using Microsoft.EntityFrameworkCore;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.ChampionShips;
using Parkside.Infrastructure.Repositories.Stuffs;
using Parkside.Infrastructure.Repositories.StuffsHistories;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;
using System.Data;

namespace Parkside.Services.StuffHistory
{
    public class StuffHistoryService : IStuffHistoryService
    {
        private readonly IStuffsHistoryRepo _stuffHistoryRepo;
        private readonly IChampionshipRepo _championshipRepo;
        private readonly IStuffRepo _stuffRepo;
        private readonly IGenericService _genericService;
        public StuffHistoryService(IStuffsHistoryRepo stuffHistoryRepo,
            IStuffRepo stuffRepo,
            IChampionshipRepo championshipRepo,
            IGenericService genericService)
        {
            _stuffHistoryRepo = stuffHistoryRepo;
            _stuffRepo = stuffRepo;
            _championshipRepo = championshipRepo;
            _genericService = genericService;
        }

        public async Task<StuffHistoryDetailsViewModel> GetStuffHistory(int id)
        {
            var stuffHistory = await _stuffHistoryRepo.GetAllStuffsHistories().FirstOrDefaultAsync(x => x.Id == id);

            if (stuffHistory == null)
                throw new NotFoundException("StuffHistory not found!");

            var finalStuffHistory = new StuffHistoryDetailsViewModel
            {
                Id = stuffHistory.Id,

                Stuff = new StuffBasicViewModel
                {
                    Id = stuffHistory.Stuff.Id,

                },
                Championship = new ChampionshipViewModel
                {
                    Id = stuffHistory.Championship.Id,
                    Name = stuffHistory.Championship.Name,

                },
                TeamName = stuffHistory.TeamName,
                Year = stuffHistory.Year,
                Role = stuffHistory.Role,
            };

            return finalStuffHistory;
        }

        public PagingViewModel<StuffHistoryViewModel> GetStuffHistories(
            string? NameSearch, string? OrderBy, string? Year, string? TeamName,
            int PageNumber, int PageSize)
        {
            var stuffHistories = _stuffHistoryRepo.GetAllStuffsHistories();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim().ToLower();
                stuffHistories = stuffHistories.Where(c => c.Stuff.FirstName.ToLower().Contains(NameSearch)
                || c.Stuff.LastName.ToLower().Contains(NameSearch) || c.Championship.Name.ToLower().Contains(NameSearch));
            }

            if (!string.IsNullOrWhiteSpace(Year))
            {
                Year = Year.Trim().ToLower();
                stuffHistories = stuffHistories.Where(c => c.Year.Contains(Year));
            }

            if (!string.IsNullOrWhiteSpace(TeamName))
            {
                TeamName = TeamName.Trim().ToLower();
                stuffHistories = stuffHistories.Where(c => c.TeamName.Contains(TeamName));
            }


            switch (OrderBy)
            {
                case ("stuffName"):
                    stuffHistories = stuffHistories.OrderBy(c => c.Stuff.LastName);
                    break;

                case ("stuffName_desc"):
                    stuffHistories = stuffHistories.OrderByDescending(c => c.Stuff.LastName);
                    break;

                case ("year"):
                    stuffHistories = stuffHistories.OrderBy(c => c.Year);
                    break;

                case ("year_desc"):
                    stuffHistories = stuffHistories.OrderByDescending(c => c.Year);
                    break;

                default:
                    stuffHistories = stuffHistories.OrderByDescending(c => c.Year);
                    break;
            }

            var numberOfStuffHistorys = stuffHistories.Count();

            var stuffHistoriessPerPage = stuffHistories.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(stuffHistory => new StuffHistoryViewModel
              {
                  Id = stuffHistory.Id,
                  StuffId = stuffHistory.Stuff.Id,
                  StuffFirstName = stuffHistory.Stuff.FirstName,
                  StuffLastName = stuffHistory.Stuff.LastName,
                  ChampionshipName = stuffHistory.Championship.Name,
                  Year = stuffHistory.Year,
                  TeamName = stuffHistory.TeamName,
                  Role = stuffHistory.Role,
                  Height = stuffHistory.Stuff.Height,
                  Description = stuffHistory.Stuff.Description,
                  Nationality = stuffHistory.Stuff.Nationality,
                  BirthDate = stuffHistory.Stuff.BirthDate.HasValue ? stuffHistory.Stuff.BirthDate.Value.ToString("dd/MM/yyyy") : null,
                  StuffImageBase64 = _genericService.GetImgBase64(stuffHistory.Stuff.ImageUrl),
                  ChampionshipImageBase64 = _genericService.GetImgBase64(stuffHistory.Championship.ImageUrl),
              })
              .ToList();

            var paginingList = new PagingViewModel<StuffHistoryViewModel>
            {
                Count = numberOfStuffHistorys,
                NumberOfPages = (int)Math.Ceiling(numberOfStuffHistorys / (double)PageSize),
                Items = stuffHistoriessPerPage
            };

            return paginingList;
        }

        public async Task AddStuffHistory(int stuffId, int championshipId, StuffHistoryCreateViewModel model)
        {
            if (await _stuffRepo.GetAsync(stuffId) == null)
            {
                throw new Exception("Stuff not found!");
            }
            if (await _championshipRepo.GetAsync(championshipId) == null)
            {
                throw new Exception("Championship not found!");
            }

            var championshipExist = await _stuffHistoryRepo.GetAllStuffsHistories().FirstOrDefaultAsync(
                x => x.ChampionshipId == championshipId && x.StuffId == stuffId && x.Year == model.Year);
            if (championshipExist != null)
            {
                throw new Exception("Championship already used!");
            }

            var finalStuffHistory = new Infrastructure.Entities.StuffHistory()
            {
                StuffId = stuffId,
                ChampionshipId = championshipId,
                TeamName = model.TeamName,
                Year = model.Year,
                Role = model.Role,
            };

            await _stuffHistoryRepo.Add(finalStuffHistory);
        }
        public async Task DeleteStuffHistory(int id)
        {
            var stuffHistory = await _stuffHistoryRepo.GetAsync(id);
            if (stuffHistory == null)
                throw new NotFoundException("StuffHistory not found!");

            await _stuffHistoryRepo.Delete(id);
        }

        public async Task VirtualDeleteStuffHistory(int id)
        {
            var stuffHistory = await _stuffHistoryRepo.GetAsync(id);
            if (stuffHistory == null)
                throw new NotFoundException("StuffHistory not found!");

            stuffHistory.IsDeleted = true;

            await _stuffHistoryRepo.Update(stuffHistory);
        }

        public async Task UpdateStuffHistory(int stuffHistoryId, int stuffId, int championshipId,
            StuffHistoryUpdateViewModel model)
        {
            var stuffHistory = await _stuffHistoryRepo.GetAsync(stuffHistoryId);

            if (stuffHistory == null)
            {
                throw new NotFoundException("StuffHistory not found!");
            }

            if (await _stuffRepo.GetAsync(stuffId) == null)
            {
                throw new Exception("Stuff not found!");
            }

            if (await _championshipRepo.GetAsync(championshipId) == null)
            {
                throw new Exception("Championship not found!");
            }

            var championshipExist = await _stuffHistoryRepo.GetAllStuffsHistories().FirstOrDefaultAsync(
                x => x.ChampionshipId == championshipId && x.StuffId == stuffId && x.Year == model.Year);
            if (championshipExist != null)
            {
                throw new Exception("Championship already used!");
            }

            stuffHistory.StuffId = stuffId;
            stuffHistory.ChampionshipId = championshipId;
            stuffHistory.TeamName = model.TeamName;
            stuffHistory.Year = model.Year;
            stuffHistory.Role = model.Role;

            await _stuffHistoryRepo.Update(stuffHistory);
        }

        public IQueryable<StuffHistoryChampionshipsViewModel> GetHomePageStuffHistory(int stuffId)
        {
            var stuffHistories = _stuffHistoryRepo.GetAllStuffsHistories().Where(x => x.StuffId == stuffId);

            var finalStuffHistorys = stuffHistories.Select(stuffHistory => new StuffHistoryChampionshipsViewModel
            {
                ChampionshipName = stuffHistory.Championship.Name,
                Year = stuffHistory.Year,
                TeamName = stuffHistory.TeamName,
                ChampionshipImageBase64 = _genericService.GetImgBase64(stuffHistory.Championship.ImageUrl),
            });

            return finalStuffHistorys;
        }
        public IQueryable<StuffHistoryDropDownViewModel> GetStuffHistoryDropDown()
        {
            var stuffHistories = _stuffHistoryRepo.GetAllStuffsHistories();

            var finalStuffHistorys = stuffHistories.Select(stuffHistory => new StuffHistoryDropDownViewModel
            {
                Id = stuffHistory.Id,
                StuffHistoryName = stuffHistory.Stuff.LastName + " " + stuffHistory.Stuff.FirstName
                + " - " + stuffHistory.Championship.Name + " - " + stuffHistory.Year
            });

            return finalStuffHistorys;
        }
    }
}

