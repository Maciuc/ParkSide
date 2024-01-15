using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.Coaches
{
    public class CoachRepo : GenericRepository<Coach>, ICoachRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public CoachRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }

    }
}
