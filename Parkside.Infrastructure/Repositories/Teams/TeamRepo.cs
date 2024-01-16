using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.Teams
{
    public class TeamRepo : GenericRepository<Team>, ITeamRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public TeamRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }

    }
}
