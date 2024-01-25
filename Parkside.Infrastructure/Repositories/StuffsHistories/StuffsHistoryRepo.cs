using Microsoft.EntityFrameworkCore;
using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.StuffsHistories
{
    public class StuffsHistoryRepo : GenericRepository<StuffHistory>, IStuffsHistoryRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public StuffsHistoryRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }
        public IQueryable<StuffHistory> GetAllStuffsHistories()
        {
            var stuffsHistories = _parksideContext.StuffHistories.Include(x => x.Stuff).Include(y => y.Championship);
            return stuffsHistories;
        }

    }
}
