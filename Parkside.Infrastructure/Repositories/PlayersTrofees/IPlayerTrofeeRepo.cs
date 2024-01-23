using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.PlayersTrofees
{
    public interface IPlayerTrofeeRepo : IGenericRepository<PlayersTrofee>
    {
        IQueryable<PlayersTrofee> GetAllPlayersTrofees();
    }
}
