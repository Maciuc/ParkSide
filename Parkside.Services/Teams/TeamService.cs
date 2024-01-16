using exp.NET6.Services.DBServices;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Teams;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;

namespace Parkside.Services.Teams
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepo _teamRepo;
        private readonly IGenericService _genericService;
        public TeamService(ITeamRepo teamRepo, IGenericService genericService)
        {
            _teamRepo = teamRepo;
            _genericService = genericService;
        }

        public async Task<TeamViewModel> GetTeam(int id)
        {
            var team = await _teamRepo.GetAsync(id);

            if (team == null)
                throw new NotFoundException("Team not found!");

            var finalTeam = new TeamViewModel
            {
                Id = team.Id,
                Name = team.Name,
                ImageBase64 = _genericService.GetImgBase64(team.ImageUrl)
            };

            return finalTeam;
        }

        public PagingViewModel<TeamViewModel> GetTeams(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize)
        {
            var teams = _teamRepo.GetAllQuerable();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim();
                teams = teams.Where(c => c.Name.Contains(NameSearch));
            }

            switch (OrderBy)
            {

                case ("name"):
                    teams = teams.OrderBy(c => c.Name);
                    break;

                case ("name_desc"):
                    teams = teams.OrderByDescending(c => c.Name);
                    break;

                default:
                    teams = teams.OrderBy(c => c.Name);
                    break;
            }

            var numberOfTeams = teams.Count();

            var teamssPerPage = teams.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(team => new TeamViewModel
              {
                  Id = team.Id,
                  Name = team.Name,
                  ImageBase64 = _genericService.GetImgBase64(team.ImageUrl)
              })
              .ToList();

            var paginingList = new PagingViewModel<TeamViewModel>
            {
                Count = numberOfTeams,
                NumberOfPages = (int)Math.Ceiling(numberOfTeams / (double)PageSize),
                Items = teamssPerPage
            };

            return paginingList;
        }

        public async Task AddTeam(TeamCreateViewModel model)
        {

            var finalTeam = new Team()
            {
                Name = model.Name,
                ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Teams")
            };

            await _teamRepo.Add(finalTeam);
        }
        public async Task DeleteTeam(int id)
        {
            var team = await _teamRepo.GetAsync(id);
            if (team == null)
                throw new NotFoundException("Team not found!");

            await _teamRepo.Delete(id);
        }

        public async Task VirtualDeleteTeam(int id)
        {
            var team = await _teamRepo.GetAsync(id);
            if (team == null)
                throw new NotFoundException("Team not found!");

            team.IsDeleted = true;

            await _teamRepo.Update(team);
        }

        public async Task UpdateTeam(int id, TeamUpdateViewModel model)
        {
            var team = await _teamRepo.GetAsync(id);

            if (team == null)
                throw new NotFoundException("Team not found!");

            team.Name = model.Name;
            team.ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Teams");

            await _teamRepo.Update(team);
        }
    }
}

