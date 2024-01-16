using System.ComponentModel.DataAnnotations;

namespace Parkside.Models.ViewModels
{
    public class CoachViewModel
    {
        public int Id { get; set; }
        public string? TeamName { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Height { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ImageBase64 { get; set; }
    }

    public class CoachUpdateViewModel
    {
        [Required(ErrorMessage = "You should provide a firstname value.")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "You should provide a lastname value.")]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        public string? TeamName { get; set; }
        public string? Description { get; set; }
        public string? Height { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ImageBase64 { get; set; }
    }

    public class CoachCreateViewModel
    {
        [Required(ErrorMessage = "You should provide a firstname value.")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "You should provide a lastname value.")]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        public string? TeamName { get; set; }
        public string? Description { get; set; }
        public string? Height { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
