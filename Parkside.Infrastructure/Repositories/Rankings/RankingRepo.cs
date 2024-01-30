using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.Rankings
{
    public class RankingRepo : GenericRepository<Ranking>, IRankingRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public RankingRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }

    }
}
