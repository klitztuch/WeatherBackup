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
        private readonly string _location;
        private readonly Config _config;
        public OpenWeatherConnector()
        {
            _config = new Config();
            _apiKey = _config.OpenWeatherMapApiKey;
            _location = _config.Location;
        }

        /// <summary>
        /// Gets the weather for the location parameter. 
        /// If no location is given, location is taken from the config
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public Weather GetWeather(string location = null)
        {
            Console.WriteLine("Getting Weather:");
            if (location == null)
            {
                location = _location;
            }
            var stringResult = GetStringResultAsync(location).Result;
            var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(GetStringResultAsync(location).Result);
            return new Weather
            {
                Location = rawWeather.Name,
                Temperature = rawWeather.Main.Temp,
                Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                Date = DateTime.Now
            };

        }

        public async Task<string> GetStringResultAsync(string location)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={location}&appid={_apiKey}&units=metric");
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