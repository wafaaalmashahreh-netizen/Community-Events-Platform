using Community_Events_Platform.Models;
using System;
using System.Collections.Generic;// to use Collections such as list<>
using System.Text;

namespace Community_Events_Platform.Helpers
{
  public  class ConsoleHelper
    {
        public static void PrintTitle(string title)
        {
            Console.WriteLine();
            Console.WriteLine("=== " + title + " ===");
        }

        public static void PrintEvents(List<Event> events)
        {
            if (events.Count == 0)
            {
                Console.WriteLine("No events found.");
                return;
            }

            foreach (var e in events)
            {
                Console.WriteLine($"[{e.EventId}] {e.Title} | {e.StartDate:dd/MM/yyyy} | {e.Location} | {e.Category} | {e.Status}");
            }
        }

        public static void PrintEventDetails(Event e)
        {
            Console.WriteLine($"Title:        {e.Title}");
            Console.WriteLine($"Description:  {e.Description}");
            Console.WriteLine($"Category:     {e.Category}");
            Console.WriteLine($"Location:     {e.Location}");
            Console.WriteLine($"Starts:       {e.StartDate:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Ends:         {e.EndDate:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Capacity:     {e.Capacity}");
            Console.WriteLine($"Status:       {e.Status}");

            if (string.IsNullOrWhiteSpace(e.RegesterInfo))
                Console.WriteLine("Registration: Not required");
            else
                Console.WriteLine($"Registration: {e.RegesterInfo}");
        }

        public static void PrintComments(List<Comment> comments)
        {
            if (comments.Count == 0)
            {
                Console.WriteLine("No comments yet.");
                return;
            }

            foreach (var c in comments)
            {
                string edited = c.IsEdited ? " (edited)" : "";
                Console.WriteLine($"[{c.CommentId}] User {c.UserId}: {c.Text}{edited}");
            }
        }
        // Prints the community requests (newest first).
        public static void PrintRequests(List<EventRequest> requests)
        {
            if (requests.Count == 0)
            {
                Console.WriteLine("No requests yet.");
                return;
            }

            foreach (var r in requests)
            {
                Console.WriteLine($"- [{r.CreatedAt:dd/MM/yyyy}] User {r.UserId}: {r.Text}");
            }
        }
    }
}
