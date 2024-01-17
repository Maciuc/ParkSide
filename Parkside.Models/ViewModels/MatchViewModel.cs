namespace Parkside.Models.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public string? ChampionshipName { get; set; }
        public string? EnemyTeamName { get; set; }
        public string? EnemyTeamImageBase64 { get; set; }
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public bool? PlayingHome { get; set; }
        public string? EnemyTeamPoints { get; set; }
        public string? MainTeamPoints { get; set; }
    }

    public class MatchUpdateViewModel
    {
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public bool PlayingHome { get; set; }
        public string? EnemyTeamPoints { get; set; }
        public string? MainTeamPoints { get; set; }
    }

    public class MatchCreateViewModel
    {
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public bool PlayingHome { get; set; }
        public string? EnemyTeamPoints { get; set; }
        public string? MainTeamPoints { get; set; }
    }

    public class MatchDetailsViewModel
    {
        public int Id { get; set; }
        public ChampionshipViewModel? Championship { get; set; }
        public TeamViewModel? EnemyTeam { get; set; }
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public bool? PlayingHome { get; set; }
        public string? EnemyTeamPoints { get; set; }
        public string? MainTeamPoints { get; set; }
    }
}
