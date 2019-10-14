using System;

namespace WeatherBackup.Model
{
    public class Weather
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Temperature { get; set; }
        public string Summary { get; set; }
    }
}