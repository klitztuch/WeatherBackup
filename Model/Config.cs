using System.Xml;

namespace WeatherBackup.Model
{
    public class Config
    {
        /// <summary>
        /// Gets or sets the OpenWeatherMap API Key
        /// </summary>
        public string OpenWeatherMapApiKey { get; set; }

        public void ReadConfig(string configLocation = "config.xml")
        {
            var doc = new XmlDocument()
            {
                PreserveWhitespace = true
            };
            try
            {
                doc.Load("config.xml");
            }
            catch (System.IO.FileNotFoundException)
            {
                System.Console.WriteLine("Can't find Configfile");
                throw;
            }
        }
    }
}