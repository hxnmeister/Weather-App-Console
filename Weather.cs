using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Linq;

namespace Weather_App
{
    internal class Weather
    {
        private readonly string API_KEY;
        private readonly string API_URL;

        public Weather(string API_URL = "https://api.weatherapi.com/v1/forecast.xml", string API_KEY = "ca4b2195d9d947638a965036233107") 
        { 
            this.API_KEY = API_KEY;
            this.API_URL = API_URL;
        }

        public void ShowWeather(string cityName, string time)
        {
            try
            {
                string webEndPoint = $"{API_URL}?key={API_KEY}&q={cityName}&days=2";
                string tommorow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

                WebClient receiver = new WebClient();
                XDocument weatherData = XDocument.Parse(Encoding.UTF8.GetString(receiver.DownloadData(webEndPoint)));
                XElement root = weatherData.Root;
                XElement forecastDay = root.Element("forecast").Elements("forecastday").FirstOrDefault(x => x.Element("date").Value == tommorow);


            }
            catch(WebException we)
            {
                Console.WriteLine("\n An exception was thrown while retrieving information from the site!");
                Console.WriteLine($" Exception Message: {we.Message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n An exception occurred during execution!");
                Console.WriteLine($" Exception Message: {ex.Message}\n");
            }
        }
    }
}
