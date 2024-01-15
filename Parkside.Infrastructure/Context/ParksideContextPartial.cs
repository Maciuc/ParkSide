using Microsoft.EntityFrameworkCore;
using Parkside.Infrastructure.Entities;

namespace Parkside.Infrastructure.Context
{
    public partial class ParksideContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Sponsor>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<SocialMedia>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<News>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Coach>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
