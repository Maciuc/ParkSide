using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class PlayersTrofee
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TrofeeId { get; set; }
        public int ChampionshipId { get; set; }
        public DateTime? TrofeeDate { get; set; }
        public string? PlayerRole { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Championship Championship { get; set; } = null!;
        public virtual Player Player { get; set; } = null!;
        public virtual Trofee Trofee { get; set; } = null!;
    }
}
