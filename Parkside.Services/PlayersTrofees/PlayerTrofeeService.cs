using exp.NET6.Services.DBServices;
using Microsoft.EntityFrameworkCore;
using Parkside.Infrastructure.Repositories.ChampionShips;
using Parkside.Infrastructure.Repositories.Players;
using Parkside.Infrastructure.Repositories.PlayersTrofees;
using Parkside.Infrastructure.Repositories.Teams;
using Parkside.Infrastructure.Repositories.Trofees;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;
using System.Data;

namespace Parkside.Services.PlayerTrofees
{
    public class PlayerTrofeeService : IPlayerTrofeeService
    {
        private readonly IPlayerTrofeeRepo _playerTrofeeRepo;
        private readonly IChampionshipRepo _championshipRepo;
        private readonly IPlayerRepo _playerRepo;
        private readonly ITrofeeRepo _trofeeRepo;
        private readonly IGenericService _genericService;
        public PlayerTrofeeService(IPlayerTrofeeRepo playerTrofeeRepo,
            IPlayerRepo playerRepo,
            ITrofeeRepo trofeeRepo,
            IChampionshipRepo championshipRepo,
            IGenericService genericService)
        {
            _playerTrofeeRepo = playerTrofeeRepo;
            _playerRepo = playerRepo;
            _trofeeRepo = trofeeRepo;
            _championshipRepo = championshipRepo;
            _genericService = genericService;
        }

        public async Task<PlayerTrofeeDetailsViewModel> GetPlayerTrofee(int id)
        {
            var playerTrofee = await _playerTrofeeRepo.GetAllPlayersTrofees().FirstOrDefaultAsync(x => x.Id == id);

            if (playerTrofee == null)
                throw new NotFoundException("PlayerTrofee not found!");

            var finalPlayerTrofee = new PlayerTrofeeDetailsViewModel
            {
                /*Id = playerTrofee.Id,
                PlayerHistory = new PlayerHistoryDropDownViewModel
                {
                    HistoryId = playerTrofee.PlayerHistoryId,
                    HistoryName = playerTrofee.PlayerHistory.
                }*/
            };

            return finalPlayerTrofee;
        }

        public PagingViewModel<PlayerTrofeeViewModel> GetPlayerTrofeees(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize)
        {
            var playerTrofeees = _playerTrofeeRepo.GetAllPlayersTrofees();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim();
                playerTrofeees = playerTrofeees.Where(c => c.Trofee.Name.Contains(NameSearch));
            }

            switch (OrderBy)
            {
                case ("playerName"):
                    playerTrofeees = playerTrofeees.OrderBy(c => c.);
                    break;

                case ("playerName_desc"):
                    playerTrofeees = playerTrofeees.OrderByDescending(c => c.Player.LastName);
                    break;

                default:
                    playerTrofeees = playerTrofeees.OrderByDescending(c => c.TrofeeDate);
                    break;
            }

            var numberOfPlayerTrofees = playerTrofeees.Count();

            var playerTrofeeessPerPage = playerTrofeees.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(playerTrofee => new PlayerTrofeeViewModel
              {
                  Id = playerTrofee.Id,
                  TrofeeName = playerTrofee.Trofee.Name,
                  PlayerFirstName = playerTrofee.Player.FirstName,
                  PlayerLastName = playerTrofee.Player.LastName,
                  ChampionshipName = playerTrofee.Championship.Name,
                  PlayerRole = playerTrofee.PlayerRole,
                  TrofeeYear = playerTrofee.TrofeeYear,
                  TrofeeImageBase64 = _genericService.GetImgBase64(playerTrofee.Trofee.ImageUrl),
                  PlayerImageBase64 = _genericService.GetImgBase64(playerTrofee.Player.ImageUrl),
                  ChampionshipImageBase64 = _genericService.GetImgBase64(playerTrofee.Championship.ImageUrl),
              })
              .ToList();

            var paginingList = new PagingViewModel<PlayerTrofeeViewModel>
            {
                Count = numberOfPlayerTrofees,
                NumberOfPages = (int)Math.Ceiling(numberOfPlayerTrofees / (double)PageSize),
                Items = playerTrofeeessPerPage
            };

            return paginingList;
        }

        public async Task AddPlayerTrofee(int trofeeId, int playerId, int championshipId, PlayerTrofeeCreateViewModel model)
        {
            if (await _trofeeRepo.GetAsync(trofeeId) == null)
            {
                throw new Exception("Trofee not found!");
            }
            if (await _playerRepo.GetAsync(playerId) == null)
            {
                throw new Exception("Player not found!");
            }
            if (await _championshipRepo.GetAsync(championshipId) == null)
            {
                throw new Exception("Player not found!");
            }

            var finalPlayerTrofee = new Infrastructure.Entities.PlayersTrofee()
            {
                TrofeeId = trofeeId,
                PlayerId = playerId,
                ChampionshipId = championshipId,
                TrofeeDate = model.TrofeeDate,
                PlayerRole = model.PlayerRole
            };

            await _playerTrofeeRepo.Add(finalPlayerTrofee);
        }
        public async Task DeletePlayerTrofee(int id)
        {
            var playerTrofee = await _playerTrofeeRepo.GetAsync(id);
            if (playerTrofee == null)
                throw new NotFoundException("PlayerTrofee not found!");

            await _playerTrofeeRepo.Delete(id);
        }

        public async Task VirtualDeletePlayerTrofee(int id)
        {
            var playerTrofee = await _playerTrofeeRepo.GetAsync(id);
            if (playerTrofee == null)
                throw new NotFoundException("PlayerTrofee not found!");

            playerTrofee.IsDeleted = true;

            await _playerTrofeeRepo.Update(playerTrofee);
        }

        public async Task UpdatePlayerTrofee(int playerTrofeeId, int trofeeId, int playerId, int championshipId,
            PlayerTrofeeUpdateViewModel model)
        {
            var playerTrofee = await _playerTrofeeRepo.GetAsync(playerTrofeeId);

            if (playerTrofee == null)
                throw new NotFoundException("PlayerTrofee not found!");

            if (await _trofeeRepo.GetAsync(trofeeId) == null)
            {
                throw new Exception("Trofee not found!");
            }
            if (await _playerRepo.GetAsync(playerId) == null)
            {
                throw new Exception("Player not found!");
            }
            if (await _championshipRepo.GetAsync(championshipId) == null)
            {
                throw new Exception("Player not found!");
            }

            playerTrofee.TrofeeId = trofeeId;
            playerTrofee.PlayerId = playerId;
            playerTrofee.ChampionshipId = championshipId;
            playerTrofee.PlayerRole = model.PlayerRole;
            playerTrofee.TrofeeDate = model.TrofeeDate;

            await _playerTrofeeRepo.Update(playerTrofee);
        }

        public IQueryable<PlayerTrofeeViewModel> GetHomePagePlayerTrofeees()
        {
            var playerTrofeees = _playerTrofeeRepo.GetAllPlayersTrofees();

            var finalPlayerTrofees = playerTrofeees.Select(playerTrofee => new PlayerTrofeeViewModel
            {
                Id = playerTrofee.Id,
                TrofeeName = playerTrofee.Trofee.Name,
                PlayerFirstName = playerTrofee.Player.FirstName,
                PlayerLastName = playerTrofee.Player.LastName,
                ChampionshipName = playerTrofee.Championship.Name,
                PlayerRole = playerTrofee.PlayerRole,
                TrofeeDate = playerTrofee.TrofeeDate.HasValue ? playerTrofee.TrofeeDate.Value.ToString("dd/MM/yyyy") : null,
                TrofeeImageBase64 = _genericService.GetImgBase64(playerTrofee.Trofee.ImageUrl),
                PlayerImageBase64 = _genericService.GetImgBase64(playerTrofee.Player.ImageUrl),
                ChampionshipImageBase64 = _genericService.GetImgBase64(playerTrofee.Championship.ImageUrl),
            });

            return finalPlayerTrofees;
        }
    }
}

