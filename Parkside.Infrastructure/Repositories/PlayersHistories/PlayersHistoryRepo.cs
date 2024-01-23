using Microsoft.EntityFrameworkCore;
using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.PlayersHistories
{
    public class PlayersHistoryRepo : GenericRepository<PlayersHistory>, IPlayersHistoryRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public PlayersHistoryRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }
        public IQueryable<PlayersHistory> GetAllPlayersHistories()
        {
            var playersHistories = _parksideContext.PlayersHistories.Include(x => x.Player).Include(y => y.Championship);
            return playersHistories;
        }

    }
}
