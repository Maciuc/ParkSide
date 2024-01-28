using System.ComponentModel.DataAnnotations;

namespace Parkside.Models.ViewModels
{
    public class SponsorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Link { get; set; }
        public string? ImageBase64 { get; set; }
    }

    public class SponsorUpdateViewModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        public string? Link { get; set; }
        public string? ImageBase64 { get; set; }
    }

    public class SponsorCreateViewModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        public string? Link { get; set; }
        public string? ImageBase64 { get; set; }
    }
}