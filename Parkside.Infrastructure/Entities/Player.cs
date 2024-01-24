using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class Player
    {
        public Player()
        {
            PlayersHistories = new HashSet<PlayersHistory>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Height { get; set; }
        public string? Number { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Nationality { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<PlayersHistory> PlayersHistories { get; set; }
    }
}
