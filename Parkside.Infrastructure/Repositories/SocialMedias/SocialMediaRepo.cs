using Parkside.Infrastructure.Context;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Generic;

namespace Parkside.Infrastructure.Repositories.SocialMedias
{
    public class SocialMediaRepo : GenericRepository<SocialMedia>, ISocialMediaRepo
    {
        public ParksideContext _parksideContext { get; set; }
        public SocialMediaRepo(ParksideContext context) : base(context)
        {
            _parksideContext = context;
        }

    }
}
