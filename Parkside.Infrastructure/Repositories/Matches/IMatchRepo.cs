using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.Matches
{
    public interface IMatchRepo : IGenericRepository<Match>
    {
        IQueryable<Match> GetAllMatches();
    }
}
