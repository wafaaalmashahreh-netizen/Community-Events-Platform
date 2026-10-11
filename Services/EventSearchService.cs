using System;
using System.Collections.Generic;
using System.Linq;
using Community_Events_Platform.Interfaces;
using Community_Events_Platform.Models;

namespace Community_Events_Platform.Services
{
    public class EventSearchService : IEventSearchService
    {
        private readonly IEventService eventService;

        public EventSearchService(IEventService eventService)
        {
           this. eventService = eventService;
        }

        public List<Event> Search(Predicate<Event> filter)
        {
            return eventService.GetAllEvents()
                                .Where(e => filter(e))
                                .ToList();
        }

        public List<Event> Search(Predicate<Event> filter, Comparison<Event> sortRule)
        {
            var results = Search(filter);
            results.Sort(sortRule);
            return results;
        }
    }
}