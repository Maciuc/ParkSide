namespace Parkside.Models.ViewModels
{
    public class PlayerTrofeeViewModel
    {
        public int Id { get; set; }
        public string? PlayerImageBase64 { get; set; }
        public string? PlayerFirstName { get; set; }
        public string? PlayerLastName { get; set; }
        public string? TrofeeImageBase64 { get; set; }
        public string? TrofeeName { get; set; }
        public string? ChampionshipImageBase64 { get; set; }
        public string? ChampionshipName { get; set; }
        public string? PlayerRole { get; set; }
        public string? TrofeeDate { get; set; }
    }

    public class PlayerTrofeeUpdateViewModel
    {
        public string? PlayerRole { get; set; }
        public DateTime? TrofeeDate { get; set; }

    }

    public class PlayerTrofeeCreateViewModel
    {
        public string? PlayerRole { get; set; }
        public DateTime? TrofeeDate { get; set; }

    }

    public class PlayerTrofeeDetailsViewModel
    {
        public int Id { get; set; }
        public ChampionshipViewModel? Championship { get; set; }
        public PlayerBasicViewModel? EnemyTeam { get; set; }
        public DateTime? PlayerTrofeeDate { get; set; }
        //blabla vedem cum se face
    }
}
