﻿using exp.NET6.Services.DBServices;
using Microsoft.EntityFrameworkCore;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.ChampionShips;
using Parkside.Infrastructure.Repositories.Players;
using Parkside.Infrastructure.Repositories.PlayersHistories;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;
using System.Data;

namespace Parkside.Services.PlayerHistory
{
    public class PlayerHistoryService : IPlayerHistoryService
    {
        private readonly IPlayersHistoryRepo _playerHistoryRepo;
        private readonly IChampionshipRepo _championshipRepo;
        private readonly IPlayerRepo _playerRepo;
        private readonly IGenericService _genericService;
        public PlayerHistoryService(IPlayersHistoryRepo playerHistoryRepo,
            IPlayerRepo playerRepo,
            IChampionshipRepo championshipRepo,
            IGenericService genericService)
        {
            _playerHistoryRepo = playerHistoryRepo;
            _playerRepo = playerRepo;
            _championshipRepo = championshipRepo;
            _genericService = genericService;
        }

        public async Task<PlayerHistoryDetailsViewModel> GetPlayerHistory(int id)
        {
            var playerHistory = await _playerHistoryRepo.GetAllPlayersHistories().FirstOrDefaultAsync(x => x.HistoryId == id);

            if (playerHistory == null)
                throw new NotFoundException("PlayerHistory not found!");

            var finalPlayerHistory = new PlayerHistoryDetailsViewModel
            {
                HistoryId = playerHistory.HistoryId,
                Player = new PlayerBasicViewModel
                {
                    Id = playerHistory.Player.Id,
                    FirstName = playerHistory.Player.FirstName,
                    LastName = playerHistory.Player.LastName,

                },
                Championship = new ChampionshipViewModel
                {
                    Id = playerHistory.Championship.Id,
                    Name = playerHistory.Championship.Name,

                },
                PlayerRole = playerHistory.PlayerRole,
                Year = playerHistory.Year,
            };

            return finalPlayerHistory;
        }

        public PagingViewModel<PlayerHistoryViewModel> GetPlayerHistories(
            string? NameSearch, string? OrderBy, int PageNumber, int PageSize)
        {
            var playerHistories = _playerHistoryRepo.GetAllPlayersHistories();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim().ToLower();
                playerHistories = playerHistories.Where(c => c.Player.FirstName.ToLower().Contains(NameSearch)
                || c.Player.LastName.ToLower().Contains(NameSearch));
            }


            switch (OrderBy)
            {
                case ("playerName"):
                    playerHistories = playerHistories.OrderBy(c => c.Player.LastName);
                    break;

                case ("playerName_desc"):
                    playerHistories = playerHistories.OrderByDescending(c => c.Player.LastName);
                    break;

                case ("year"):
                    playerHistories = playerHistories.OrderBy(c => c.Year);
                    break;

                case ("year_desc"):
                    playerHistories = playerHistories.OrderByDescending(c => c.Year);
                    break;

                default:
                    playerHistories = playerHistories.OrderByDescending(c => c.Year);
                    break;
            }

            var numberOfPlayerHistorys = playerHistories.Count();

            var playerHistoriessPerPage = playerHistories.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(playerHistory => new PlayerHistoryViewModel
              {
                  HistoryId = playerHistory.HistoryId,
                  PlayerFirstName = playerHistory.Player.FirstName,
                  PlayerLastName = playerHistory.Player.LastName,
                  ChampionshipName = playerHistory.Championship.Name,
                  PlayerRole = playerHistory.PlayerRole,
                  Year = playerHistory.Year,
                  PlayerImageBase64 = _genericService.GetImgBase64(playerHistory.Player.ImageUrl),
                  ChampionshipImageBase64 = _genericService.GetImgBase64(playerHistory.Championship.ImageUrl),
              })
              .ToList();

            var paginingList = new PagingViewModel<PlayerHistoryViewModel>
            {
                Count = numberOfPlayerHistorys,
                NumberOfPages = (int)Math.Ceiling(numberOfPlayerHistorys / (double)PageSize),
                Items = playerHistoriessPerPage
            };

            return paginingList;
        }

        public async Task AddPlayerHistory(int playerId, int championshipId, PlayerHistoryCreateViewModel model)
        {
            if (await _playerRepo.GetAsync(playerId) == null)
            {
                throw new Exception("Player not found!");
            }
            if (await _championshipRepo.GetAsync(championshipId) == null)
            {
                throw new Exception("Championship not found!");
            }

            var championshipExist = await _playerHistoryRepo.GetAllPlayersHistories().FirstOrDefaultAsync(x => x.ChampionshipId == championshipId);
            if (championshipExist != null)
            {
                throw new Exception("Championship already used!");
            }

            var finalPlayerHistory = new PlayersHistory()
            {
                PlayerId = playerId,
                ChampionshipId = championshipId,
                PlayerRole = model.PlayerRole,
                Year = model.Year,
            };

            await _playerHistoryRepo.Add(finalPlayerHistory);
        }
        public async Task DeletePlayerHistory(int id)
        {
            var playerHistory = await _playerHistoryRepo.GetAsync(id);
            if (playerHistory == null)
                throw new NotFoundException("PlayerHistory not found!");

            await _playerHistoryRepo.Delete(id);
        }

        public async Task VirtualDeletePlayerHistory(int id)
        {
            var playerHistory = await _playerHistoryRepo.GetAsync(id);
            if (playerHistory == null)
                throw new NotFoundException("PlayerHistory not found!");

            playerHistory.IsDeleted = true;

            await _playerHistoryRepo.Update(playerHistory);
        }

        public async Task UpdatePlayerHistory(int playerHistoryId, int playerId, int championshipId,
            PlayerHistoryUpdateViewModel model)
        {
            var playerHistory = await _playerHistoryRepo.GetAsync(playerHistoryId);

            if (playerHistory == null)
            {
                throw new NotFoundException("PlayerHistory not found!");
            }

            if (await _playerRepo.GetAsync(playerId) == null)
            {
                throw new Exception("Player not found!");
            }

            if (await _championshipRepo.GetAsync(championshipId) == null)
            {
                throw new Exception("Championship not found!");
            }

            var championshipExist = await _playerHistoryRepo.GetAllPlayersHistories().FirstOrDefaultAsync(x => x.ChampionshipId == championshipId);
            if (championshipExist != null)
            {
                throw new Exception("Championship already used!");
            }

            playerHistory.PlayerId = playerId;
            playerHistory.ChampionshipId = championshipId;
            playerHistory.PlayerRole = model.PlayerRole;
            playerHistory.Year = model.Year;

            await _playerHistoryRepo.Update(playerHistory);
        }

        public IQueryable<PlayerHistoryChampionshipsViewModel> GetHomePagePlayerHistory(int playerId)
        {
            var playerHistories = _playerHistoryRepo.GetAllPlayersHistories().Where(x => x.PlayerId == playerId);

            var finalPlayerHistorys = playerHistories.Select(playerHistory => new PlayerHistoryChampionshipsViewModel
            {
                ChampionshipName = playerHistory.Championship.Name,
                PlayerRole = playerHistory.PlayerRole,
                Year = playerHistory.Year,
                ChampionshipImageBase64 = _genericService.GetImgBase64(playerHistory.Championship.ImageUrl),
            });

            return finalPlayerHistorys;
        }
        public IQueryable<PlayerHistoryDropDownViewModel> GetPlayerHistoryDropDown()
        {
            var playerHistories = _playerHistoryRepo.GetAllPlayersHistories();

            var finalPlayerHistorys = playerHistories.Select(playerHistory => new PlayerHistoryDropDownViewModel
            {
                HistoryId = playerHistory.HistoryId,
                HistoryName = playerHistory.Player.LastName + " " + playerHistory.Player.FirstName
                + " - " + playerHistory.Championship.Name + " - " + playerHistory.Year
            });

            return finalPlayerHistorys;
        }
    }
}

