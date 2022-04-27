using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheStillHeron.TestWorkshop.Console.Weather
{
    public class WeatherResponse
    {
        [JsonPropertyName("weather")]
        public IList<WeatherValue> Weather { get; set; }

        [JsonPropertyName("main")]
        public WeatherDetail Main { get; set; }
    }

    public class WeatherValue
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("main")]
        public string Main { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class WeatherDetail
    {
        [JsonPropertyName("temp")]
        public float Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public float FeelsLike { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }
}