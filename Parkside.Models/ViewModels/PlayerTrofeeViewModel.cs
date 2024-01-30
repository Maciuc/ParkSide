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
        public string? TeamName { get; set; }
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }
    }
    public class PlayerTrofeeHomeViewModel
    {
        public int Id { get; set; }
        public string? TrofeeImageBase64 { get; set; }
        public string? TrofeeName { get; set; }
        public string? ChampionshipImageBase64 { get; set; }
        public string? ChampionshipName { get; set; }
        public string? TeamName { get; set; }
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }
    }

    public class PlayerTrofeeDetailsViewModel
    {
        public int Id { get; set; }
        public PlayerHistoryDropDownViewModel? PlayerHistory { get; set; }
        public TrofeeViewModel? Trofee { get; set; }
    }
}
