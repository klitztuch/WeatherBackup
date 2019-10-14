using LiteDB;
using WeatherBackup.Model;
using System.Collections.Generic;

namespace WeatherBackup
{
    public class LiteDbConnector
    {
        private readonly string _dbName;
        public LiteDbConnector(string dbName)
        {
            _dbName = dbName;
            using (var db = new LiteDatabase(_dbName))
            {
                var weatherCollection = db.GetCollection<Weather>();
                weatherCollection.EnsureIndex(x => x.Location);
                weatherCollection.EnsureIndex(x => x.Temperature);
                weatherCollection.EnsureIndex(x => x.Summary);
            }
        }

        public void InsertMultipleData(IEnumerable<Weather> data)
        {
            using (var db = new LiteDatabase(_dbName))
            {
                var weatherCollection = db.GetCollection<Weather>();
                weatherCollection.InsertBulk(data);
            }
        }

        public void InsertSingleData(Weather data)
        {
            using (var db = new LiteDatabase(_dbName))
            {
                var weatherCollection = db.GetCollection<Weather>();
                weatherCollection.Insert(data);
            }
        }

        public void PrintAllData(object dataModel)
        {
            using (var db = new LiteDatabase(_dbName))
            {


                switch (dataModel)
                {
                    case Weather w:
                        var weatherCollection = db.GetCollection<Weather>();
                        foreach (var item in weatherCollection.FindAll())
                        {
                            System.Console.WriteLine(item.Id);
                            System.Console.WriteLine(item.Date);
                            System.Console.WriteLine(item.Location);
                            System.Console.WriteLine(item.Temperature);
                            System.Console.WriteLine(item.Summary);
                        }
                        break;

                    default:
                        System.Console.WriteLine("Can't find type");
                        break;
                }
            }
        }
    }
}