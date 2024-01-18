using System.ComponentModel.DataAnnotations;

namespace Parkside.Models.ViewModels
{
    public class TrofeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }

    public class TrofeeUpdateViewModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }

    public class TrofeeCreateViewModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }
}
