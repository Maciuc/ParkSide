using System;
using System.Collections.Generic;

namespace Parkside.Backend.Entities
{
    public partial class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Link { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
