using exp.NET6.Services.DBServices;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Players;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;

namespace Parkside.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepo _playerRepo;
        private readonly IGenericService _genericService;
        public PlayerService(IPlayerRepo playerRepo, IGenericService genericService)
        {
            _playerRepo = playerRepo;
            _genericService = genericService;
        }

        public async Task<PlayerViewModel> GetPlayer(int id)
        {
            var player = await _playerRepo.GetAsync(id);

            if (player == null)
                throw new NotFoundException("Player not found!");

            var finalPlayer = new PlayerViewModel
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                TeamName = player.TeamName,
                Height = player.Height,
                Number = player.Number,
                Description = player.Description,
                Role = player.Role,
                BirthDate = player.BirthDate,
                ImageBase64 = _genericService.GetImgBase64(player.ImageUrl)
            };

            return finalPlayer;
        }

        public PagingViewModel<PlayerViewModel> GetPlayers(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize)
        {
            var players = _playerRepo.GetAllQuerable();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim();
                players = players.Where(c => c.FirstName.Contains(NameSearch) ||
                c.LastName.Contains(NameSearch));
            }

            switch (OrderBy)
            {

                case ("name"):
                    players = players.OrderBy(c => c.LastName);
                    break;

                case ("name_desc"):
                    players = players.OrderByDescending(c => c.LastName);
                    break;

                default:
                    players = players.OrderBy(c => c.LastName);
                    break;
            }

            var numberOfPlayers = players.Count();

            var playerssPerPage = players.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(player => new PlayerViewModel
              {
                  Id = player.Id,
                  FirstName = player.FirstName,
                  LastName = player.LastName,
                  TeamName = player.TeamName,
                  Height = player.Height,
                  Description = player.Description,
                  Number = player.Number,
                  Role = player.Role,
                  BirthDate = player.BirthDate,
                  ImageBase64 = _genericService.GetImgBase64(player.ImageUrl)
              })
              .ToList();

            var paginingList = new PagingViewModel<PlayerViewModel>
            {
                Count = numberOfPlayers,
                NumberOfPages = (int)Math.Ceiling(numberOfPlayers / (double)PageSize),
                Items = playerssPerPage
            };

            return paginingList;
        }

        public async Task AddPlayer(PlayerCreateViewModel model)
        {

            var finalPlayer = new Player()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                TeamName = model.TeamName,
                Height = model.Height,
                Description = model.Description,
                Number = model.Number,
                Role = model.Role,
                BirthDate = model.BirthDate,
                ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Players")
            };

            await _playerRepo.Add(finalPlayer);
        }
        public async Task DeletePlayer(int id)
        {
            var player = await _playerRepo.GetAsync(id);
            if (player == null)
                throw new NotFoundException("Player not found!");

            await _playerRepo.Delete(id);
        }

        public async Task VirtualDeletePlayer(int id)
        {
            var player = await _playerRepo.GetAsync(id);
            if (player == null)
                throw new NotFoundException("Player not found!");

            player.IsDeleted = true;

            await _playerRepo.Update(player);
        }

        public async Task UpdatePlayer(int id, PlayerUpdateViewModel model)
        {
            var player = await _playerRepo.GetAsync(id);
            if (player == null)
                throw new NotFoundException("Player not found!");

            player.FirstName = model.FirstName;
            player.LastName = model.LastName;
            player.Height = model.Height;
            player.Number = model.Number;
            player.Description = model.Description;
            player.TeamName = model.TeamName;
            player.Role = model.Role;
            player.BirthDate = model.BirthDate;
            player.ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Players");

            await _playerRepo.Update(player);
        }

        public IQueryable<PlayerViewModel> GetHomePagePlayers()
        {
            var players = _playerRepo.GetAllQuerable();

            var finalPlayers = players.Select(player => new PlayerViewModel
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                TeamName = player.TeamName,
                Height = player.Height,
                Number = player.Number,
                Description = player.Description,
                Role = player.Role,
                BirthDate = player.BirthDate,
                ImageBase64 = _genericService.GetImgBase64(player.ImageUrl)
            });

            return finalPlayers;
        }
    }
}

