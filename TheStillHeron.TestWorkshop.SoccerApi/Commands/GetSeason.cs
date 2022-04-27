using TheStillHeron.TestWorkshop.SoccerApi.Domain;
using TheStillHeron.TestWorkshop.SoccerApi.Repository;

namespace TheStillHeron.TestWorkshop.SoccerApi.Commands
{
    public class GetSeason
    {
        private SeasonRepository _repo;

        public GetSeason(SeasonRepository repo)
        {
            _repo = repo;
        }

        public Season Execute(string year)
        {
            return _repo.Get(year);
        }
    }
}
