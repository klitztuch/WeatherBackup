using WeatherBackup.Model;

namespace WeatherBackup
{
    public class Process
    {
        public void Run()
        {
            var openWeatherConnector = new OpenWeatherConnector();
            var weather = openWeatherConnector.GetWeather();

            var liteDbConnector = new LiteDbConnector("WeatherData.db");
            liteDbConnector.InsertSingleData(weather);
            liteDbConnector.PrintAllData(weather);
        }
    }
}