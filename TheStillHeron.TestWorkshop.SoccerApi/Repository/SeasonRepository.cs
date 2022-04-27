using System;
using System.Linq;
using TheStillHeron.TestWorkshop.SoccerApi.DataStorage;
using TheStillHeron.TestWorkshop.SoccerApi.Domain;

namespace TheStillHeron.TestWorkshop.SoccerApi.Repository
{
    public class SeasonRepository
    {
        private DataStore _storage;

        public SeasonRepository(DataStore storage)
        {
            _storage = storage;
        }

        public Season GetCurrentSeason()
        {
            var currentSeason = Environment.GetEnvironmentVariable("CURRENT_SEASON");
            var season = _storage.Get<Season>(Season.TABLE_NAME, currentSeason);
            return season;
        }

        public Season Get(string year)
        {
            return _storage
                .Get<Season>(Season.TABLE_NAME)
                .Where(x => x.Year == year)
                .FirstOrDefault();
        }

        public void Put(Season season)
        {
            _storage.Put(Season.TABLE_NAME, season);
        }
    }
}