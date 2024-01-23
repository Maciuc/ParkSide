using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.PlayersHistories
{
    public interface IPlayersHistoryRepo : IGenericRepository<PlayersHistory>
    {
        IQueryable<PlayersHistory> GetAllPlayersHistories();
    }
}
