 Community Events Platform
 

#Overview

Community Events Platform is a C# console application that helps communities discover, suggest, and organize events based on people's interests and feedback.

Users can browse and search events, view details, mark Interested / Not interested, and comment. They can also propose their own events with optional registration info. Based on reactions and comments, organizers decide whether to confirm, cancel, or update their events. A public requests board lets users suggest the kinds of events they want to see.

Built for the Prime Academy C# Backend Project (2026).

## Main Features

- Sign up / Sign in with an email
- Create events (title, description, location, category, dates, capacity, optional registration info)
- Browse events and view details with reaction counts and comments
- Mark an event as Interested / Not interested (one reaction per user per event; changing it updates it)
- Comment on events
- Search by keyword, category, or upcoming date (nearest first)
- Organizer tools (own events only): stats, confirm, cancel, edit
- Public requests board
- Switch user (to test organizer permissions)

## Project Structure

```
Community_Events_Platform/
├── Models/        User, Event, Comment, EventReaction, EventRequest, EventStatus, EventCategory, ReactionType
├── Interfaces/    IUserService, IEventService, IEventSearchService, IReactionService, ICommentService, IEventRequestService
├── Services/      One service per interface + ConsoleMenu
├── Helpers/       InputHelper, ConsoleHelper
├── Extensions/    EventExtensions
└── Program.cs
```

| Folder | Role |
|---|---|
| Models| The entities. They hold data and protect their own state. |
| Interfaces | Contracts describing what each service does. |
| Services | Business logic (permissions, one reaction per user, search...). ConsoleMenu only coordinates: shows the menu, reads the choice, calls the right service. |
| Helpers | Reusable utilities with no business rules: safe console input and printing. |
| Extensions | Event.IsUpcoming() keeps the "not cancelled and not in the past" rule in one place. |
| Program.cs | Creates the objects, adds sample data, starts the menu. |

Data is stored in memory (no database), as allowed by the brief. A Repository layer was intentionally not added to keep the project simple; Dependency Inversion is applied through the service interfaces.

## Where OOP Is Demonstrated

- **Encapsulation:** models use `private set` and change state only through methods (`Event.Confirm()`, `Event.Cancel()`, `Event.UpdateDetails()`, `Comment.Edit()`, `EventReaction.ChangeType()`). Services keep their lists private and return copies.
- **Abstraction:** every service has an interface.
- **Polymorphism:** `ConsoleMenu` and `EventSearchService` depend on interfaces, so any implementation can be used in its place.
- **Composition:** `ConsoleMenu` is built from six services; `EventSearchService` has an `IEventService`.
- **Inheritance:** not used on purpose, because there is no real "is-a" relationship in this system.

## Where SOLID Is Applied

- **S:** each service has one job; printing and input are in Helpers; the menu only coordinates.
- **O:** new search behavior is added by passing a new Predicate/Comparison, without changing `EventSearchService`.
- **L:** any implementation of an interface can replace another without breaking the menu or search.
- **I:** small, focused interfaces instead of one large one.
- **D:** `ConsoleMenu` and `EventSearchService` receive interfaces through their constructors; `Program.cs` creates the concrete objects.

## Where the Delegate Is Used and Why

`IEventSearchService.Search` takes a `Predicate<Event>` (the filter) and an optional `Comparison<Event>` (the sort rule). The caller decides the behavior, so one method supports many searches:

```csharp
results = searchService.Search(
    e => e.IsUpcoming(),
    (a, b) => a.StartDate.CompareTo(b.StartDate));
```

It is used for keyword search, category search, upcoming events (nearest first), and "My events". Adding a new search needs no change to `EventSearchService`.

## How to Run

1. Install .NET SDK  10.0.
2. Clone the repository: `git clone https://github.com/wafaaalmashahreh-netizen/Community-Events-Platform.git`
3. Open in Visual Studio and press Run (use the project profile, not Docker), or run `dotnet run` in the project folder.
4. Sample accounts: `sara@mail.com` and `ali@mail.com`, or sign up with a new email.

Dates are entered as `dd/MM/yyyy` (for example `25/12/2026`).

## Repository

- GitHub: https://github.com/wafaaalmashahreh-netizen/Community-Events-Platform
- Version tag: `V1.0.0`

Author: [Wafaa almashahreh ]



