using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.ChampionShips
{
    public class ChampionshipRepo : GenericRepository<Championship>, IChampionshipRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public ChampionshipRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }

    }
}
