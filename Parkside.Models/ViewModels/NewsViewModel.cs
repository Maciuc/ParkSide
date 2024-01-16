using System.ComponentModel.DataAnnotations;

namespace Parkside.Models.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageBase64 { get; set; }
        public string? PublishedDate { get; set; }
        public bool? IsPublished { get; set; }
    }

    public class NewsUpdateViewModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImageBase64 { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool? IsPublished { get; set; }
    }

    public class NewsCreateViewModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImageBase64 { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool? IsPublished { get; set; }
    }
    public class NewsDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageBase64 { get; set; }
        public string? Content { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool? IsPublished { get; set; }
    }
}