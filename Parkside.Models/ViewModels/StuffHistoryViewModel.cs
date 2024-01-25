﻿namespace Parkside.Models.ViewModels
{
    public class StuffHistoryViewModel
    {
        public int Id { get; set; }
        public string? StuffImageBase64 { get; set; }
        public string? StuffFirstName { get; set; }
        public string? StuffLastName { get; set; }
        public string? ChampionshipImageBase64 { get; set; }
        public string? ChampionshipName { get; set; }
        public string? TeamName { get; set; }
        public string? Year { get; set; }

    }
    public class StuffHistoryUpdateViewModel
    {
        public string? TeamName { get; set; }
        public string? Year { get; set; }

    }
    public class StuffHistoryCreateViewModel
    {
        public string? TeamName { get; set; }
        public string? Year { get; set; }

    }

    public class StuffHistoryDetailsViewModel
    {
        public int Id { get; set; }
        public StuffBasicViewModel? Stuff { get; set; }
        public ChampionshipViewModel? Championship { get; set; }
        public string? TeamName { get; set; }
        public string? Year { get; set; }

    }
    public class StuffHistoryChampionshipsViewModel
    {
        public string? ChampionshipImageBase64 { get; set; }
        public string? ChampionshipName { get; set; }
        public string? TeamName { get; set; }
        public string? Year { get; set; }

    }

    public class StuffHistoryDropDownViewModel
    {
        public int Id { get; set; }
        public string? StuffHistoryName { get; set; }

    }

    /*public class StuffHistoryChampionshipsListViewModel
    {
        public int StuffId { get; set; }
        public StuffHistoryChampionshipsViewModel? Championships { get; set; }

    }*/
}
