using System;

namespace TheStillHeron.TestWorkshop.SoccerApi.Domain
{
    public enum Winner
    {
        Home,
        Away
    }

    public class Match
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public DateTime MatchTime { get; set; }

        public Winner? Winner { get; set; }

        public int MatchNumber { get; set; }

        public void SetWinner(Winner newWinner)
        {
            if (Winner != null)
            {
                throw new Exception("Match is already over");
            }

            Winner = newWinner;
        }
    }
}