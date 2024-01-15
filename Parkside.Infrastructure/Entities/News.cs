using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class News
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
