using exp.NET6.Services.DBServices;
using Microsoft.EntityFrameworkCore;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.ChampionShips;
using Parkside.Infrastructure.Repositories.Players;
using Parkside.Infrastructure.Repositories.PlayersHistories;
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
        private readonly IPlayersHistoryRepo _playerHistoryRepo;
        private readonly ITrofeeRepo _trofeeRepo;
        private readonly IGenericService _genericService;
        public PlayerTrofeeService(IPlayerTrofeeRepo playerTrofeeRepo,
            IPlayersHistoryRepo playerHistoryRepo,
            ITrofeeRepo trofeeRepo,
            IGenericService genericService)
        {
            _playerTrofeeRepo = playerTrofeeRepo;
            _playerHistoryRepo = playerHistoryRepo;
            _trofeeRepo = trofeeRepo;
            _genericService = genericService;
        }

        /*public async Task<PlayerTrofeeDetailsViewModel> GetPlayerTrofee(int id)
        {
            var playerTrofee = await _playerTrofeeRepo.GetAllPlayersTrofees().FirstOrDefaultAsync(x => x.Id == id);

            if (playerTrofee == null)
                throw new NotFoundException("PlayerTrofee not found!");

            var finalPlayerTrofee = new PlayerTrofeeDetailsViewModel
            {
                *//*Id = playerTrofee.Id,
                PlayerHistory = new PlayerHistoryDropDownViewModel
                {
                    HistoryId = playerTrofee.PlayerHistoryId,
                    HistoryName = playerTrofee.PlayerHistory.
                }*//*
            };

            return finalPlayerTrofee;
        }*/

        public PagingViewModel<PlayerTrofeeViewModel> GetPlayerTrofees(
            string? NameSearch, string? OrderBy, string? Year, int PageNumber, int PageSize)
        {
            var playerTrofeees = _playerTrofeeRepo.GetAllPlayersTrofees();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim();
                playerTrofeees = playerTrofeees.Where(c => c.Trofee.Name.Contains(NameSearch) || 
                c.PlayerHistory.Championship.Name.Contains(NameSearch) || c.PlayerHistory.Player.FirstName.Contains(NameSearch) ||
                c.PlayerHistory.Player.LastName.Contains(NameSearch));
            }

            if (!string.IsNullOrWhiteSpace(Year))
            {
                Year = Year.Trim().ToLower();
                playerTrofeees = playerTrofeees.Where(c => c.PlayerHistory.Year.Contains(Year));
            }

            switch (OrderBy)
            {
                case ("playerName"):
                    playerTrofeees = playerTrofeees.OrderBy(c => c.PlayerHistory.Player.LastName);
                    break;

                case ("playerName_desc"):
                    playerTrofeees = playerTrofeees.OrderByDescending(c => c.PlayerHistory.Player.LastName);
                    break;

                case ("year"):
                    playerTrofeees = playerTrofeees.OrderBy(c => c.PlayerHistory.Year);
                    break;

                case ("year_desc"):
                    playerTrofeees = playerTrofeees.OrderByDescending(c => c.PlayerHistory.Year);
                    break;

                default:
                    playerTrofeees = playerTrofeees.OrderByDescending(c => c.PlayerHistory.Year);
                    break;
            }

            var numberOfPlayerTrofees = playerTrofeees.Count();

            var playerTrofeeessPerPage = playerTrofeees.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(playerTrofee => new PlayerTrofeeViewModel
              {
                  Id = playerTrofee.Id,
                  TrofeeName = playerTrofee.Trofee.Name,
                  PlayerFirstName = playerTrofee.PlayerHistory.Player.FirstName,
                  PlayerLastName = playerTrofee.PlayerHistory.Player.LastName,
                  ChampionshipName = playerTrofee.PlayerHistory.Championship.Name,
                  PlayerRole = playerTrofee.PlayerHistory.PlayerRole,
                  Year = playerTrofee.PlayerHistory.Year,
                  TeamName = playerTrofee.PlayerHistory.TeamName,
                  TrofeeImageBase64 = _genericService.GetImgBase64(playerTrofee.Trofee.ImageUrl),
                  PlayerImageBase64 = _genericService.GetImgBase64(playerTrofee.PlayerHistory.Player.ImageUrl),
                  ChampionshipImageBase64 = _genericService.GetImgBase64(playerTrofee.PlayerHistory.Championship.ImageUrl),
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

        public async Task AddPlayerTrofee(int playerHistoryId, int trofeeId)
        {
            if (await _trofeeRepo.GetAsync(trofeeId) == null)
            {
                throw new Exception("Trofee not found!");
            }
            if (await _playerHistoryRepo.GetAsync(playerHistoryId) == null)
            {
                throw new Exception("PlayerHistory not found!");
            }

            var finalPlayerTrofee = new PlayersTrofee()
            {
                PlayerHistoryId = playerHistoryId,
                TrofeeId = trofeeId
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

        /*public async Task UpdatePlayerTrofee(int playerTrofeeId, int trofeeId, int playerId, int championshipId,
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
        }*/

        public IQueryable<PlayerTrofeeHomeViewModel> GetHomePagePlayerTrofees(int playerId)
        {
            var playerTrofeees = _playerTrofeeRepo.GetAllPlayersTrofees().Where(x => x.PlayerHistory.Player.Id == playerId);

            var finalPlayerTrofees = playerTrofeees.Select(playerTrofee => new PlayerTrofeeHomeViewModel
            {
                Id = playerTrofee.Id,
                TrofeeName = playerTrofee.Trofee.Name,
                ChampionshipName = playerTrofee.PlayerHistory.Championship.Name,
                PlayerRole = playerTrofee.PlayerHistory.PlayerRole,
                Year = playerTrofee.PlayerHistory.Year,
                TeamName = playerTrofee.PlayerHistory.TeamName,
                TrofeeImageBase64 = _genericService.GetImgBase64(playerTrofee.Trofee.ImageUrl),
                ChampionshipImageBase64 = _genericService.GetImgBase64(playerTrofee.PlayerHistory.Championship.ImageUrl),
            });

            return finalPlayerTrofees;
        }
    }
}

