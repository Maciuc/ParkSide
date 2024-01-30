using System;
using System.Collections.Generic;

namespace Parkside.Infrastructure.Entities
{
    public partial class Ranking
    {
        public int Id { get; set; }
        public string? Pos { get; set; }
        public string? Echipa { get; set; }
        public string? Juc { get; set; }
        public string? V { get; set; }
        public string? E { get; set; }
        public string? P { get; set; }
        public string? Gm { get; set; }
        public string? Gp { get; set; }
        public string? Gdif { get; set; }
        public string? Va { get; set; }
        public string? Ea { get; set; }
        public string? Vd { get; set; }
        public string? Ed { get; set; }
        public string? PtsA { get; set; }
        public string? PtsD { get; set; }
        public string? Pts { get; set; }
    }
}
