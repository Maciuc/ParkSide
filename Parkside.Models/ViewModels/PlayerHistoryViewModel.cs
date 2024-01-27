namespace Parkside.Models.ViewModels
{
    public class PlayerHistoryViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string? PlayerImageBase64 { get; set; }
        public string? PlayerFirstName { get; set; }
        public string? PlayerLastName { get; set; }
        public string? ChampionshipImageBase64 { get; set; }
        public string? ChampionshipName { get; set; }
        public string? PlayerRole { get; set; }
        public string? TeamName { get; set; }
        public string? Year { get; set; }
        public string? Number { get; set; }
        public string? Height { get; set; }
        public string? Description { get; set; }
        public string? BirthDate { get; set; }
        public string? Nationality { get; set; }

    }
    public class PlayerHistoryUpdateViewModel
    {
        public string? TeamName { get; set; }
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }

    }
    public class PlayerHistoryCreateViewModel
    {
        public string? TeamName { get; set; }
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }

    }

    public class PlayerHistoryDetailsViewModel
    {
        public int Id { get; set; }
        public PlayerBasicViewModel? Player { get; set; }
        public ChampionshipViewModel? Championship { get; set; }
        public string? TeamName { get; set; }
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }

    }
    public class PlayerHistoryChampionshipsViewModel
    {
        public string? ChampionshipImageBase64 { get; set; }
        public string? ChampionshipName { get; set; }
        public string? TeamName { get; set; }
        public string? PlayerRole { get; set; }
        public string? Year { get; set; }

    }

    public class PlayerHistoryDropDownViewModel
    {
        public int Id { get; set; }
        public string? PlayerHistoryName { get; set; }

    }

    /*public class PlayerHistoryChampionshipsListViewModel
    {
        public int PlayerId { get; set; }
        public PlayerHistoryChampionshipsViewModel? Championships { get; set; }

    }*/
}
