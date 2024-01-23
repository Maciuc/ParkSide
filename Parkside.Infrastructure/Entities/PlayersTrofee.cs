using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class PlayersTrofee
    {
        public int Id { get; set; }
        public int PlayerHistoryId { get; set; }
        public int TrofeeId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual PlayersHistory PlayerHistory { get; set; } = null!;
        public virtual Trofee Trofee { get; set; } = null!;
    }
}
