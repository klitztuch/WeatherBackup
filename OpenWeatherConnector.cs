using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using WeatherBackup.Model;

namespace WeatherBackup
{
    public class OpenWeatherConnector
    {
        private readonly string _apiKey;
        private readonly Config _config;
        public OpenWeatherConnector()
        {
            _config = new Config();
            _apiKey = _config.OpenWeatherMapApiKey;
        }
        public Weather GetWeather(string city)
        {
            Console.WriteLine("Getting Weather:");
            var stringResult = GetStringResultAsync(city).Result;
            var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(GetStringResultAsync(city).Result);
            return new Weather
            {
                Location = rawWeather.Name,
                Temperature = rawWeather.Main.Temp,
                Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                Date = DateTime.Now
            };

        }

        public async Task<string> GetStringResultAsync(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();

                }
                catch (HttpRequestException)
                {
                    throw;
                }
            }
        }
    }
}