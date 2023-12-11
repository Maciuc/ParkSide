using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.Players
{
    public class PlayerRepo : GenericRepository<Player>, IPlayerRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public PlayerRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }

    }
}
