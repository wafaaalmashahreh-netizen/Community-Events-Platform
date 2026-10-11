using Community_Events_Platform.Interfaces;
using Community_Events_Platform.Services;
using System;
using Community_Events_Platform.Models;

namespace Community_Events_Platform
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserService userService = new UserService();
            IEventService eventService = new EventService();
            IEventSearchService searchService = new EventSearchService(eventService);
            IReactionService reactionService = new ReactionService();
            ICommentService commentService = new CommentService();
            IEventRequestService requestService = new EventRequestService();

            var menu = new ConsoleMenu(userService, eventService, searchService,
                                       reactionService, commentService, requestService);
           
            // Sample data so the app isn't empty on start
            var sara = userService.Register("sara", "sara@mail.com");
            var ali = userService.Register("Ali", "ali@mail.com");

            eventService.CreateEvent(sara!.UserId, "Community Cooking Day", "Cook and share recipes",
                "Bethlahem", EventCategory.Cooking,
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(5), 30, "https://forms.gle/example");

            eventService.CreateEvent(sara.UserId, "Book Club Meetup", "Monthly reading discussion",
                "Rammalah", EventCategory.Reading,
                DateTime.Today.AddDays(10), DateTime.Today.AddDays(10), 15, null);

            eventService.CreateEvent(ali!.UserId, "Kids Art Workshop", "Painting for children",
                "Jerico", EventCategory.Children,
                DateTime.Today.AddDays(3), DateTime.Today.AddDays(3), 20, null);

            requestService.AddRequest(ali.UserId, "We would like a photography workshop");

            menu.Run();
        }

    }
}