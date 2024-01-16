using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class Match
    {
        public int Id { get; set; }
        public int ChampionshipId { get; set; }
        public int EnemyTeamId { get; set; }
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public bool? PlayingHome { get; set; }
        public int? EnemyTeamPoints { get; set; }
        public int? MainTeamPoints { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Championship Championship { get; set; } = null!;
        public virtual Team EnemyTeam { get; set; } = null!;
    }
}
