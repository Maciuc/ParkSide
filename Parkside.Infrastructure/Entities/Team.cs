using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class Team
    {
        public Team()
        {
            Matches = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
