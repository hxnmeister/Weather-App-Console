using System;
using System.Linq;
using System.Text;
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

        public void Show(string cityName, string time)
        {
            try
            {
                string webEndPoint = $"{API_URL}?key={API_KEY}&q={cityName}&days=2";
                string tommorow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

                WebClient receiver = new WebClient();
                XDocument weatherData = XDocument.Parse(Encoding.UTF8.GetString(receiver.DownloadData(webEndPoint)));
                XElement location = weatherData.Root.Element("location");
                XElement forecastForDay = weatherData.Root.Element("forecast").Elements("forecastday").FirstOrDefault(x => x.Element("date").Value == tommorow);

                Console.WriteLine($"\n  Weather for {location.Element("name").Value}, {location.Element("region").Value}, {location.Element("country").Value}");
                Console.WriteLine($"\n  {tommorow}");
                Console.WriteLine("  ================================");
                Console.WriteLine($"  Max Temperature: {forecastForDay.Element("day").Element("maxtemp_c").Value}°C / {forecastForDay.Element("day").Element("maxtemp_f").Value}°F");
                Console.WriteLine($"  Min Temperature: {forecastForDay.Element("day").Element("mintemp_c").Value}°C / {forecastForDay.Element("day").Element("mintemp_f").Value}°F");
                Console.WriteLine($"  Max Wind Speed: {forecastForDay.Element("day").Element("maxwind_kph").Value} km/h");
                Console.WriteLine($"  Average Visibility: {forecastForDay.Element("day").Element("avgvis_km").Value} km/h");
                Console.WriteLine("  ================================\n\n");

                if (time == null)
                {
                    foreach (XElement item in forecastForDay.Elements("hour"))
                    {
                        ShowCurrentForecast(item);
                    }
                }
                else
                {
                    ShowCurrentForecast(forecastForDay.Elements("hour").FirstOrDefault(x => x.Element("time").Value == $"{tommorow} {time}"));
                }
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

        private void ShowCurrentForecast(XElement currentForecast)
        {
            bool willItRain = currentForecast.Element("will_it_rain").Value == "1";
            bool willItSnow = currentForecast.Element("chance_of_snow").Value == "1";

            Console.ForegroundColor = Console.ForegroundColor == ConsoleColor.Yellow ? ConsoleColor.Blue : ConsoleColor.Yellow;

            Console.WriteLine("  ============================");
            Console.WriteLine($"  Forecast on {currentForecast.Element("time").Value}");
            Console.WriteLine("  ============================");
            Console.WriteLine($"  Temperature: {currentForecast.Element("temp_c").Value}°C / {currentForecast.Element("temp_f").Value}°F");
            Console.WriteLine($"  Feels Like: {currentForecast.Element("feelslike_c").Value}°C / {currentForecast.Element("feelslike_f").Value}°F");
            Console.WriteLine($"  UV Index: {currentForecast.Element("uv").Value}");
            Console.WriteLine("  --");
            Console.WriteLine($"  Wind Speed: {currentForecast.Element("wind_kph").Value} km/h");
            Console.WriteLine($"  Wind Direction: {currentForecast.Element("wind_dir").Value}");
            Console.WriteLine("  --");
            Console.WriteLine($"  Visibility: {currentForecast.Element("vis_km").Value} km/h");
            Console.WriteLine($"  Cloudiness: {currentForecast.Element("cloud").Value}%");
            Console.WriteLine($"  Humidity: {currentForecast.Element("humidity").Value}%");
            Console.WriteLine("  --");
            Console.WriteLine($"  Will It Rain: {(willItRain ? "Yes" : "No")}");
            if (willItRain) Console.WriteLine($"  Chance Of Rain: {currentForecast.Element("chance_of_rain").Value}%");
            Console.WriteLine($"  Will It Snow: {(currentForecast.Element("will_it_snow").Value == "1" ? "Yes" : "No")}");
            if (willItSnow) Console.WriteLine($"  Chance Of Snow: {currentForecast.Element("chance_of_snow").Value}%");
            Console.WriteLine("  ============================\n");
        }
    }
}
