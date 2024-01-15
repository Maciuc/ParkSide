using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class Player
    {
        public int Id { get; set; }
        public string? TeamName { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Height { get; set; }
        public int? Number { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Role { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
