using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.Sponsors
{
    public class SponsorRepo : GenericRepository<Sponsor>, ISponsorRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public SponsorRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }

    }
}
