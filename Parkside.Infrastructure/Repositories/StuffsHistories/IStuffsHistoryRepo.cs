using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.StuffsHistories
{
    public interface IStuffsHistoryRepo : IGenericRepository<StuffHistory>
    {
        IQueryable<StuffHistory> GetAllStuffsHistories();
    }
}
