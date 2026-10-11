using System;
using System.Collections.Generic;
using System.Text;

namespace Community_Events_Platform.Helpers
{
    public  class InputHelper
    {

        public static string ReadString(string message)
        {
            Console.Write(message);
            string? text = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(text))
            {
                Console.Write("Can't be empty, try again: ");
                text = Console.ReadLine();
            }

            return text.Trim();
        }

        // Asks for a number. If not a number, asks again.
        public static int ReadInt(string message)
        {
            Console.Write(message);
            string? text = Console.ReadLine();
            int number;

            while (!int.TryParse(text, out number))
            {
                Console.Write("Enter a number, try again: ");
                text = Console.ReadLine();
            }

            return number;
        }

        // Asks for a date (day/month/year). If invalid, asks again.
        public static DateTime ReadDate(string message)
        {
            Console.Write(message + " (example: 25/12/2026): ");
            string? text = Console.ReadLine();
            DateTime date;

            while (!DateTime.TryParse(text, out date))
            {
                Console.Write("Invalid date, try again (example: 25/12/2026): ");
                text = Console.ReadLine();
            }

            return date.Date;
        }
    }

    }

