using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class Championship
    {
        public Championship()
        {
            Matches = new HashSet<Match>();
            PlayersHistories = new HashSet<PlayersHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<PlayersHistory> PlayersHistories { get; set; }
    }
}
