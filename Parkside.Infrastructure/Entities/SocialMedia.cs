using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class SocialMedia
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Link { get; set; }
        public bool IsDeleted { get; set; }
    }
}
