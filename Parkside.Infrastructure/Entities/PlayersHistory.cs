using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class PlayersHistory
    {
        public PlayersHistory()
        {
            PlayersTrofees = new HashSet<PlayersTrofee>();
        }

        public int HistoryId { get; set; }
        public int PlayerId { get; set; }
        public int ChampionshipId { get; set; }
        public string? Year { get; set; }
        public string? PlayerRole { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Championship Championship { get; set; } = null!;
        public virtual Player Player { get; set; } = null!;
        public virtual ICollection<PlayersTrofee> PlayersTrofees { get; set; }
    }
}
