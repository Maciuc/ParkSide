namespace Parkside.Models.ViewModels
{
    public class PlayerHistoryViewModel
    {
        public int HistoryId { get; set; }
        public string? PlayerImageBase64 { get; set; }
        public string? PlayerFirstName { get; set; }
        public string? PlayerLastName { get; set; }
        public string? ChampionshipImageBase64 { get; set; }
        public string? ChampionshipName { get; set; }
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }

    }
    public class PlayerHistoryUpdateViewModel
    {
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }

    }
    public class PlayerHistoryCreateViewModel
    {
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }

    }

    public class PlayerHistoryDetailsViewModel
    {
        public int HistoryId { get; set; }
        public PlayerBasicViewModel? Player { get; set; }
        public ChampionshipViewModel? Championship { get; set; }
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }

    }
    public class PlayerHistoryChampionshipsViewModel
    {
        public string? ChampionshipImageBase64 { get; set; }
        public string? ChampionshipName { get; set; }
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }

    }

    public class PlayerHistoryDropDownViewModel
    {
        public int HistoryId { get; set; }
        public string? HistoryName { get; set; }

    }

    /*public class PlayerHistoryChampionshipsListViewModel
    {
        public int PlayerId { get; set; }
        public PlayerHistoryChampionshipsViewModel? Championships { get; set; }

    }*/
}
