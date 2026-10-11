using System;
using Community_Events_Platform.Helpers;
using Community_Events_Platform.Interfaces;
using Community_Events_Platform.Models;
using Community_Events_Platform.Extensions;
using System.Collections.Generic;

namespace Community_Events_Platform.Services
{
    // Shows the menu, reads the user's choice, and calls the right service.
    // It only coordinates; the business logic lives in the services.
    public class ConsoleMenu
    {
        // depend on interfaces, not concrete classes
        private readonly IUserService userService;
        private readonly IEventService eventService;
        private readonly IEventSearchService searchService;
        private readonly IReactionService reactionService;
        private readonly ICommentService commentService;
        private readonly IEventRequestService requestService;

        // The logged-in user. Null until the user logs in.
        private User? currentUser;

        public ConsoleMenu(IUserService userService,
                            IEventService eventService,
                            IEventSearchService searchService,
                            IReactionService reactionService,
                            ICommentService commentService,
                            IEventRequestService requestService)
        {
            this.userService = userService;
            this.eventService = eventService;
            this.searchService = searchService;
            this.reactionService = reactionService;
            this.commentService = commentService;
            this.requestService = requestService;
        }

        // The only public method: Program.cs calls it to start the app.
        public void Run()
        {
            Login();

            bool running = true;

            // Repeat the menu until the user chooses Exit.
            while (running)
            {
                ShowMenu();
                int choice = InputHelper.ReadInt("Choose: ");

                switch (choice)
                {
                    case 1:
                        BrowseEvents();
                        break;
                    case 2:
                        CreateEvent();
                        break;
                    case 3:
                        ViewEventDetails();
                        break;

                    case 4:
                        SearchEvents();
                        break;

                    case 5:
                        ManageMyEvents();
                        break;

                    case 6:
                        RequestsBoard();
                        break;

                    case 7:
                        this.currentUser = null;
                        Login();
                        break;
                    case 0:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

            Console.WriteLine("Thanks for using Community Events Platform. See you soon!");
        }

        private void ShowMenu()
        {
            ConsoleHelper.PrintTitle("Community Events Platform");
            Console.WriteLine("1. Browse all events");
            Console.WriteLine("2. Create event");
            Console.WriteLine("3. View event details");
            Console.WriteLine("4. Search events");
            Console.WriteLine("5. Manage my events");
            Console.WriteLine("6. Requests board");
            Console.WriteLine("7. Switch user");
            Console.WriteLine("0. Exit");
        }

        // Asks for an email. If an account exists, logs in. Otherwise creates one.
        // Start screen: the user must sign in or sign up before using the app.
        private void Login()
        {
            while (this.currentUser == null)
            {
                ConsoleHelper.PrintTitle("Welcome");
                Console.WriteLine("1. Sign in");
                Console.WriteLine("2. Sign up");

                int choice = InputHelper.ReadInt("Choose: ");

                switch (choice)
                {
                    case 1:
                        SignIn();
                        break;
                    case 2:
                        SignUp();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // Asks for an email. If it is not found, the user can try again or go back.
        private void SignIn()
        {
            while (true)
            {
                string email = InputHelper.ReadString("Enter your email: ");
                User? user = this.userService.Login(email);

                if (user != null)
                {
                    this.currentUser = user;
                    Console.WriteLine($"Welcome back, {user.Name}!");
                    return;
                }

                Console.WriteLine("No account found with this email.");
                string answer = InputHelper.ReadString("Try again? (y/n): ");

                if (!answer.Equals("y", StringComparison.OrdinalIgnoreCase))
                    return;   // back to the start screen
            }
        }

        // Creates a new account. Rejects an email that is already registered.
        private void SignUp()
        {
            string name = InputHelper.ReadString("Enter your name: ");
            string email = InputHelper.ReadString("Enter your email: ");

            User? user = this.userService.Register(name, email);

            if (user == null)
            {
                Console.WriteLine("This email is already registered. Please sign in instead.");
                return;
            }

            this.currentUser = user;
            Console.WriteLine($"Account created. Welcome, {user.Name}!");
        }
        private void BrowseEvents()
        {
            ConsoleHelper.PrintEvents(this.eventService.GetAllEvents());
        }

        private void CreateEvent()
        {
            ConsoleHelper.PrintTitle("Create Event");

            string title = InputHelper.ReadString("Title: ");
            string description = InputHelper.ReadString("Description: ");
            string location = InputHelper.ReadString("Location: ");
            EventCategory category = ReadCategory();
            DateTime startDate = InputHelper.ReadDate("Start date");
            DateTime endDate = InputHelper.ReadDate("End date");
            int capacity = InputHelper.ReadInt("Capacity: ");

            Console.Write("Registration info (leave empty if none): ");
            string? registration = Console.ReadLine();

            Event created = this.eventService.CreateEvent(
                this.currentUser!.UserId, title, description, location,
                category, startDate, endDate, capacity, registration);

            Console.WriteLine($"Event created with ID {created.EventId}. Status: {created.Status}");
        }

        // Shows the categories as a numbered list and returns the one the user picks.
        private EventCategory ReadCategory()
        {
            Console.WriteLine("Categories:");

            EventCategory[] categories = Enum.GetValues<EventCategory>();

            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i]}");
            }

            int choice = InputHelper.ReadInt("Choose category number: ");

            while (choice < 1 || choice > categories.Length)
            {
                choice = InputHelper.ReadInt("Invalid. Choose category number: ");
            }

            return categories[choice - 1];
        }

        // Asks for an event id, shows its details, then lets the user react or comment.
        private void ViewEventDetails()
        {
            int id = InputHelper.ReadInt("Enter event ID: ");
            Event? evt = this.eventService.GetEventById(id);

            if (evt == null)
            {
                Console.WriteLine("Event not found.");
                return;
            }

            bool viewing = true;

            while (viewing)
            {
                ShowEventScreen(evt);

                Console.WriteLine("1. Interested");
                Console.WriteLine("2. Not interested");
                Console.WriteLine("3. Add comment");
                Console.WriteLine("0. Back");

                int choice = InputHelper.ReadInt("Choose: ");

                switch (choice)
                {
                    case 1:
                        this.reactionService.React(evt.EventId, this.currentUser!.UserId, ReactionType.Interested);
                        Console.WriteLine("Marked as Interested.");
                        break;
                    case 2:
                        this.reactionService.React(evt.EventId, this.currentUser!.UserId, ReactionType.NotInterested);
                        Console.WriteLine("Marked as Not interested.");
                        break;
                    case 3:
                        {
                            string text = InputHelper.ReadString("Your comment: ");
                            this.commentService.AddComment(evt.EventId, this.currentUser!.UserId, text);
                            Console.WriteLine("Comment added.");
                            break;
                        }

                   
                    case 0:
                        viewing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // Prints the event details, the reaction counts, and the comments.
        private void ShowEventScreen(Event evt)
        {
            ConsoleHelper.PrintTitle(evt.Title);
            ConsoleHelper.PrintEventDetails(evt);

            int interested = this.reactionService.CountReactions(evt.EventId, ReactionType.Interested);
            int notInterested = this.reactionService.CountReactions(evt.EventId, ReactionType.NotInterested);
            Console.WriteLine($"Interested: {interested} | Not interested: {notInterested}");

            Console.WriteLine("--- Comments ---");
            ConsoleHelper.PrintComments(this.commentService.GetCommentsForEvent(evt.EventId));
        }
    

  
private void SearchEvents()
        {
            ConsoleHelper.PrintTitle("Search Events");
            Console.WriteLine("1. By keyword");
            Console.WriteLine("2. By category");
            Console.WriteLine("3. Upcoming events (nearest first)");
            Console.WriteLine("0. Back");

            int choice = InputHelper.ReadInt("Choose: ");
            List<Event> results;

            switch (choice)
            {
                case 1:
                    {
                        string keyword = InputHelper.ReadString("Keyword: ");

                        // Predicate: true if the title or description contains the keyword
                        results = this.searchService.Search(e =>
                            e.Status != EventStatus.Cancelled &&
                            (e.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                             e.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)));
                        break;
                    }
                case 2:
                    {
                        EventCategory category = ReadCategory();

                        // Predicate: true if the event has the chosen category
                        results = this.searchService.Search(e =>
                            e.Status != EventStatus.Cancelled &&
                            e.Category == category);
                        break;
                    }
                
                case 3:
                    // Predicate: uses the IsUpcoming extension (not cancelled, not in the past).
                    // Comparison: the earlier start date comes first.
                    results = this.searchService.Search(
                        e => e.IsUpcoming(),
                        (a, b) => a.StartDate.CompareTo(b.StartDate));
                    break;

                default:
                    return;
            }

            ConsoleHelper.PrintEvents(results);
        }

        // Shows only the events created by the current user, then lets them pick one to manage.
        private void ManageMyEvents()
        {
            ConsoleHelper.PrintTitle("My Events");

            // Predicate: only the events I organize
            List<Event> myEvents = this.searchService.Search(e => e.UserId == this.currentUser!.UserId);

            if (myEvents.Count == 0)
            {
                Console.WriteLine("You have no events yet.");
                return;
            }

            ConsoleHelper.PrintEvents(myEvents);

            int id = InputHelper.ReadInt("Enter event ID to manage (0 to go back): ");
            if (id == 0)
                return;

            Event? evt = myEvents.Find(e => e.EventId == id);

            if (evt == null)
            {
                Console.WriteLine("That is not one of your events.");
                return;
            }

            ManageSingleEvent(evt);
        }

        private void ManageSingleEvent(Event evt)
        {
            bool managing = true;

            while (managing)
            {
                ShowOrganizerStats(evt);

                Console.WriteLine("1. Confirm event");
                Console.WriteLine("2. Cancel event");
                Console.WriteLine("3. Edit details");
                Console.WriteLine("0. Back");

                int choice = InputHelper.ReadInt("Choose: ");

                switch (choice)
                {
                    case 1:
                        if (this.eventService.ConfirmEvent(evt.EventId, this.currentUser!.UserId))
                            Console.WriteLine("Event confirmed.");
                        else
                            Console.WriteLine("Could not confirm the event.");
                        break;
                    case 2:
                        if (this.eventService.CancelEvent(evt.EventId, this.currentUser!.UserId))
                            Console.WriteLine("Event cancelled.");
                        else
                            Console.WriteLine("Could not cancel the event.");
                        break;
                    case 3:
                        EditEvent(evt);
                        break;
                    case 0:
                        managing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // The numbers the organizer uses to decide: confirm or cancel?
        private void ShowOrganizerStats(Event evt)
        {
            ConsoleHelper.PrintTitle("Manage: " + evt.Title);

            int interested = this.reactionService.CountReactions(evt.EventId, ReactionType.Interested);
            int notInterested = this.reactionService.CountReactions(evt.EventId, ReactionType.NotInterested);
            int comments = this.commentService.GetCommentsForEvent(evt.EventId).Count;

            Console.WriteLine($"Status: {evt.Status} | Capacity: {evt.Capacity}");
            Console.WriteLine($"Interested: {interested} | Not interested: {notInterested} | Comments: {comments}");
        }

        // Asks for new details and updates the event.
        private void EditEvent(Event evt)
        {
            Console.WriteLine("Enter the new details:");

            string title = InputHelper.ReadString("Title: ");
            string description = InputHelper.ReadString("Description: ");
            string location = InputHelper.ReadString("Location: ");
            EventCategory category = ReadCategory();
            DateTime startDate = InputHelper.ReadDate("Start date");
            DateTime endDate = InputHelper.ReadDate("End date");
            int capacity = InputHelper.ReadInt("Capacity: ");

            Console.Write("Registration info (leave empty if none): ");
            string? registration = Console.ReadLine();

            bool updated = this.eventService.UpdateEvent(evt.EventId, this.currentUser!.UserId,
                title, description, location, category, startDate, endDate, capacity, registration);

            Console.WriteLine(updated ? "Event updated." : "Could not update the event.");
        }

        // Shows all community requests and lets the user add a new one.
        private void RequestsBoard()
        {
            bool onBoard = true;

            while (onBoard)
            {
                ConsoleHelper.PrintTitle("Requests Board");
                ConsoleHelper.PrintRequests(this.requestService.GetAllRequests());

                Console.WriteLine("1. Add a request");
                Console.WriteLine("0. Back");

                int choice = InputHelper.ReadInt("Choose: ");

                switch (choice)
                {
                    case 1:
                        {
                            string text = InputHelper.ReadString("What kind of events would you like to see? ");
                            this.requestService.AddRequest(this.currentUser!.UserId, text);
                            Console.WriteLine("Request added.");
                            break;
                        }
                    case 0:
                        onBoard = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    } }