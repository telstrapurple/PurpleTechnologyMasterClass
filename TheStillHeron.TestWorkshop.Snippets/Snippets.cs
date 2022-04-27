using System;
using TheStillHeron.TestWorkshop.Console.Weather;

namespace TheStillHeron.TestWorkshop.Snippets
{
    // -- start snippet.1 --
    var response = new WeatherResponse
    {
        Weather = new List<WeatherValue>{
            new WeatherValue
            {
                Id = 123,
                Main = "Rain",
                Description = "Some light showers"
            }
        },
        Main = new WeatherDetail
        {
            Temp = 22.3f,
            FeelsLike = 23,
            Humidity = 78
        }
    };
    // -- end snippet.1 --
}
