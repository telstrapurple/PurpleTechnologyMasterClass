using System.Collections.Generic;
using System;
using TheStillHeron.TestWorkshop.SoccerApi.DataStorage;
using System.Collections.ObjectModel;
using System.Linq;

namespace TheStillHeron.TestWorkshop.SoccerApi.Domain
{
    public class Season : IStorable
    {
        public static string TABLE_NAME = "Season";

        public IList<Round> Rounds { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public string Year { get; set; }

        public void AddRound(Round newRound)
        {
            Rounds.Add(newRound);
        }

        public ICollection<Match> Today()
        {
            ICollection<Match> result = new Collection<Match>();
            foreach (var round in Rounds)
            {
                result = result.Concat(round.Today()) as ICollection<Match>;
            }
            return result;
        }
    }
}