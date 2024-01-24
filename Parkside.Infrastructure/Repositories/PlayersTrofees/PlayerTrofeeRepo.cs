using Microsoft.EntityFrameworkCore;
using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.PlayersTrofees
{
    public class PlayerTrofeeRepo : GenericRepository<PlayersTrofee>, IPlayerTrofeeRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public PlayerTrofeeRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }
        public IQueryable<PlayersTrofee> GetAllPlayersTrofees()
        {
            var playersTrofees = _parksideContext.PlayersTrofees.Include(x => x.PlayerHistory).Include(y => y.Trofee);
            return playersTrofees;
        }

    }
}
