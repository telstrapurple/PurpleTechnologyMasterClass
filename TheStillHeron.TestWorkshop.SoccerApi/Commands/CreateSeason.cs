using System.Collections.Generic;
using TheStillHeron.TestWorkshop.SoccerApi.Domain;
using TheStillHeron.TestWorkshop.SoccerApi.Repository;

namespace TheStillHeron.TestWorkshop.SoccerApi.Commands
{
    public class CreateSeason
    {
        private SeasonRepository _repo;

        public CreateSeason(SeasonRepository repo)
        {
            _repo = repo;
        }

        public void Execute(string name, string year)
        {
            var season = new Season
            {
                Id = year,
                Name = name,
                Year = year,
                Rounds = new List<Round>()
            };
            _repo.Put(season);
        }
    }
}
