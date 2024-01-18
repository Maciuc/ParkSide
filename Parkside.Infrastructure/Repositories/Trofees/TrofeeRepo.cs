using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.Trofees
{
    public class TrofeeRepo : GenericRepository<Trofee>, ITrofeeRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public TrofeeRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }

    }
}
