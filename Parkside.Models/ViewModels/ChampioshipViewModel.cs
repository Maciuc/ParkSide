using System.ComponentModel.DataAnnotations;

namespace Parkside.Models.ViewModels
{
    public class ChampionshipViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }

    public class ChampionshipUpdateViewModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }

    public class ChampionshipCreateViewModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? ImageBase64 { get; set; }
    }
}
