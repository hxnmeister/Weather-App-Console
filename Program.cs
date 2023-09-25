using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Weather_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cityName = null;
            DateTime? timeDay = null;

            do
            {
                Console.WriteLine("\n  To find out the weather, enter the city and time of day (leave the field blank to get an hourly forecast)");

                do
                {
                    Console.Write("\n Enter city name: ");
                    cityName = Console.ReadLine();

                    Console.Clear();
                    if (!(new Regex("^[a-zA-Z]{2,}$").Match(cityName).Success)) Console.WriteLine($"\n City name \"{cityName}\" is incorrect, try again!");
                    else break;

                } while (true);

                do
                {
                    Console.Write("\n Enter time(HH:mm): ");
                    string tempTime = Console.ReadLine();

                    Console.Clear();
                    if (string.IsNullOrWhiteSpace(tempTime))
                    {
                        break;
                    }
                    else if (DateTime.TryParse(tempTime, out DateTime result))
                    {
                        timeDay = result;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\n Entered format \"{tempTime}\" is incorrect, try again!");
                    }

                } while (true);

                new Weather().ShowWeather("Sumy", timeDay?.ToString("HH:mm"));

            } while (true);
        }
    }
}
