using System;
using System.Text.RegularExpressions;

namespace Weather_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                do
                {
                    string cityName = null;
                    Weather weather = new Weather();
                    DateTime? timeDay = null;

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\n  To find out tomorrow's weather ({DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")}), enter your city and time of day (leave blank to get an hourly forecast)");

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

                    Console.Clear();
                    weather.Show(cityName, timeDay?.ToString("HH:mm"));

                    Console.Write("\n\n To exit, press \"E\", to continue, press any key\n");
                } while (char.ToUpper(Console.ReadKey(true).KeyChar) != 'E');
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n An exception occurred during execution!");
                Console.WriteLine($" Exception Message: {ex.Message}\n");
            }
        }
    }
}
