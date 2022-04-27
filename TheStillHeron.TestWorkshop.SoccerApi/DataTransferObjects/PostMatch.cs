using System;

namespace TheStillHeron.TestWorkshop.SoccerApi.Controllers
{
    public class PostMatch
    {
        public int RoundNumber { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public DateTime MatchTime { get; set; }

        public int MatchNumber { get; set; }
    }
}
