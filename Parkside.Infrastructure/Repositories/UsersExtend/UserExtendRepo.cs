using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.UsersExtend
{
    public class UserExtendRepo : GenericRepository<UserExtend>, IUserExtendRepo
    {
        public ParksideContext _templateContext { get; set; }
        public UserExtendRepo(ParksideContext context) : base(context)
        {
            _templateContext = context;
        }
    }
}
