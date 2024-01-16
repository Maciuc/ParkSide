using System.ComponentModel.DataAnnotations;

namespace Parkside.Models.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }

    public class TeamUpdateViewModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }

    public class TeamCreateViewModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }
}
