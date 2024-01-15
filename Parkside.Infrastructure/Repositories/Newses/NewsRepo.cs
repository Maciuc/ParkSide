using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.Newses
{
    public class NewsRepo : GenericRepository<News>, INewsRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public NewsRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }

    }
}
