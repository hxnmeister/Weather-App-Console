using System;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

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
                    //The variable is initialized with the value null to be able to understand the user whether he wants to get a full forecast or for a certain time
                    DateTime? timeDay = null;

                    Console.Clear();
                    //Returning the text color after displaying the weather
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\n  To find out tomorrow's weather ({DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")}), enter your city and time of day (leave blank to get an hourly forecast)");

                    do
                    {
                        Console.Write("\n Enter city name: ");
                        cityName = Console.ReadLine();

                        Console.Clear();
                        if (!(new Regex("^[a-zA-Z -]{2,}$").Match(cityName).Success)) Console.WriteLine($"\n City name \"{cityName}\" is incorrect, try again!");
                        else break;

                    } while (true);

                    //This check is aimed at finding out from the user what kind of summary he wants to receive, complete or accurate in time
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
                    weather.Display(cityName, timeDay?.ToString("HH" + ":00")) ;

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
