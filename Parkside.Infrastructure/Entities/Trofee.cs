using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class Trofee
    {
        public Trofee()
        {
            PlayersTrofees = new HashSet<PlayersTrofee>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<PlayersTrofee> PlayersTrofees { get; set; }
    }
}
