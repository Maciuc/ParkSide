using System;
using System.Collections.Generic;

namespace Parkside.Backend.Entities
{
    public partial class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Height { get; set; }
        public int? Number { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
