using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TheStillHeron.TestWorkshop.Console.Weather
{
    // ex.1
    public class WeatherApiClient
    {
        private HttpClient _httpClient;
        private string _latitude;
        private string _longitude;
        private string _apiKey;

        public WeatherApiClient(IConfiguration config)
        {
            _httpClient = new HttpClient();
            var configValue = config.GetValue<string>("WeatherApiBaseUrl");
            _httpClient.BaseAddress = new Uri(config.GetValue<string>("WeatherApiBaseUrl"));
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _latitude = config.GetValue<string>("Latitude");
            _longitude = config.GetValue<string>("Longitude");
            _apiKey = config.GetValue<string>("WeatherApiKey");
        }

        public async Task<WeatherResponse> GetCurrentWeather()
        {
            var response = await _httpClient
                .GetAsync($"weather?lat={_latitude}&lon={_longitude}&appid={_apiKey}&units=metric");

            return await ParseResponseAs<WeatherResponse>(response);
        }

        private async Task<T> ParseResponseAs<T>(HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseAsString);
        }
    }
}