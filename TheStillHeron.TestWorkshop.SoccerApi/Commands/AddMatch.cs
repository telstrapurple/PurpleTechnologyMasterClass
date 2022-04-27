using System.Linq;
using TheStillHeron.TestWorkshop.SoccerApi.Repository;
using TheStillHeron.TestWorkshop.SoccerApi.Exceptions;
using TheStillHeron.TestWorkshop.SoccerApi.Domain;
using System;

namespace TheStillHeron.TestWorkshop.SoccerApi.Commands
{
    public class AddMatch
    {
        private SeasonRepository _repo;

        public AddMatch(SeasonRepository repo)
        {
            _repo = repo;
        }

        public void Execute(string seasonYear, int roundNumber, string homeTeam, string awayTeam, DateTime matchTime, int matchNumber)
        {
            var currentSeason = _repo.Get(seasonYear);
            var round = currentSeason
                .Rounds.Where(x => x.RoundNumber == roundNumber)
                .FirstOrDefault();

            if (round == null)
            {
                throw new NotFoundException();
            }

            var match = new Match
            {
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                MatchTime = matchTime,
                MatchNumber = matchNumber
            };
            round.AddMatch(match);

            _repo.Put(currentSeason);
        }
    }
}
