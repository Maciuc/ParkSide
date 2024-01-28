using System.ComponentModel.DataAnnotations;

namespace Parkside.Models.ViewModels
{
    public class StuffViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Nationality { get; set; }
        public string? Height { get; set; }
        public string? BirthDate { get; set; }
        public string? ImageBase64 { get; set; }
    }

    public class StuffUpdateViewModel
    {
        [Required(ErrorMessage = "You should provide a firstname value.")]
        [MaxLength(200)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "You should provide a lastname value.")]
        [MaxLength(200)]
        public string LastName { get; set; } = null!;
        public string? Nationality { get; set; }
        public string? Description { get; set; }
        public string? Height { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ImageBase64 { get; set; }
    }

    public class StuffCreateViewModel
    {
        [Required(ErrorMessage = "You should provide a firstname value.")]
        [MaxLength(200)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "You should provide a lastname value.")]
        [MaxLength(200)]
        public string LastName { get; set; } = null!;
        public string? Nationality { get; set; }
        public string? Description { get; set; }
        public string? Height { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ImageBase64 { get; set; }
    }

    public class StuffDetailsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Nationality { get; set; }
        public string? Height { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ImageBase64 { get; set; }
    }

    public class StuffBasicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
