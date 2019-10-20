using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

namespace WeatherBackup.Model
{
    public class Config
    {
        /// <summary>
        /// Gets or sets the OpenWeatherMap API Key
        /// </summary>
        public string OpenWeatherMapApiKey { get; set; }
        /// <summary>
        /// Gets or sets the Location
        /// </summary>
        /// <value></value>
        public string Location { get; set; }
        public Config()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            OpenWeatherMapApiKey = configuration["OpenWeatherMapApiKey"];
            Location = configuration["Location"];
        }
    }
}