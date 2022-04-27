using System.Collections.Generic;
using TheStillHeron.TestWorkshop.SoccerApi.Domain;
using TheStillHeron.TestWorkshop.SoccerApi.Repository;

namespace TheStillHeron.TestWorkshop.SoccerApi.Commands
{
    public class AddRound
    {
        private SeasonRepository _repo;

        public AddRound(SeasonRepository repo)
        {
            _repo = repo;
        }

        public void Execute(string currentSeasonYear, int roundNumber)
        {
            var currentSeason = _repo.Get(currentSeasonYear);
            var round = new Round { RoundNumber = roundNumber, Matches = new List<Match>() };
            currentSeason.AddRound(round);
            _repo.Put(currentSeason);
        }
    }
}
