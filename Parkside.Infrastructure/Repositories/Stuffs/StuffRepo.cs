using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.Stuffs
{
    public class StuffRepo : GenericRepository<Stuff>, IStuffRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public StuffRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }

    }
}
