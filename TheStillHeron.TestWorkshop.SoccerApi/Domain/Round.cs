using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace TheStillHeron.TestWorkshop.SoccerApi.Domain
{
    public class Round
    {
        public IList<Match> Matches { get; set; }

        public int RoundNumber { get; set; }

        public void AddMatch(Match newMatch)
        {
            Matches.Add(newMatch);
        }

        public ICollection<Match> Today()
        {
            var result = new Collection<Match>();

            foreach (var match in Matches)
            {
                if (match.MatchTime.Date == DateTime.Today)
                {
                    result.Add(match);
                }
            }

            return result;
        }
    }
}