using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class Stuff
    {
        public Stuff()
        {
            StuffHistories = new HashSet<StuffHistory>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Height { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Nationality { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<StuffHistory> StuffHistories { get; set; }
    }
}
