using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TheStillHeron.TestWorkshop.Console.Weather;

namespace TheStillHeron.TestWorkshop.Console
{
    public class RecommendationEngine
    {
        private IConfiguration _config;

        public RecommendationEngine(IConfiguration config)
        {
            // ex.1
            _config = config;
        }
        public async Task<string> GetRecommendation()
        {
            // ex.1
            var apiClient = new WeatherApiClient(_config);
            var currentWeather = await apiClient.GetCurrentWeather();
            var feelsLike = currentWeather.Main.FeelsLike;
            var conditions = currentWeather.Weather.Select(x => x.Main).Aggregate((left, right) => $"{left} {right}");

            if (conditions.Contains("Thunderstorm"))
            {
                return "Might be better to stay at home!";
            }
            if (conditions.Contains("Clear") && feelsLike > 15 && feelsLike < 30)
            {
                return "Looks like a beautiful day";
            }
            if (conditions.Contains("Rain") || conditions.Contains("Drizzle"))
            {
                return "Don't forget your umbrella!";
            }
            if (conditions.Contains("Snow") || feelsLike < 10)
            {
                return "Be sure to rug up, it's nippy out there";
            }
            return "Use your best judgement";
        }
    }
}