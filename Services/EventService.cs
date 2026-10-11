using System;
using System.Collections.Generic;
using System.Linq;
using Community_Events_Platform.Interfaces;
using Community_Events_Platform.Models;

namespace Community_Events_Platform.Services
{
    public class EventService : IEventService
    {
        private readonly List<Event> events = new();
        private int nextId = 1;

        public Event CreateEvent(int userId, string title, string description,
                                 string location, EventCategory category,
                                 DateTime startDate, DateTime endDate,
                                 int capacity, string? regesterInfo)
        {
            var evt = new Event(nextId++, title, userId, startDate, endDate,
                                description, capacity, regesterInfo , location, category);
            events.Add(evt);
            return evt;
        }

        public bool UpdateEvent(int eventId, int userId, string title, string description,
                                string location, EventCategory category,
                                DateTime startDate, DateTime endDate,
                                int capacity, string? regesterInfo)
        {
            var evt = GetEventById(eventId);
            if (evt == null || evt.UserId != userId)
                return false;

            evt.UpdateDetails(title, description, location, category,
                              startDate, endDate, capacity, regesterInfo);
            return true;
        }

        public bool ConfirmEvent(int eventId, int userId)
        {
            var evt = GetEventById(eventId);
            if (evt == null || evt.UserId != userId)
                return false;

            evt.Confirm();
            return true;
        }

        public bool CancelEvent(int eventId, int userId)
        {
            var evt = GetEventById(eventId);
            if (evt == null || evt.UserId != userId)
                return false;

            evt.Cancel();
            return true;
        }

        public Event? GetEventById(int eventId)
        {
            return events.FirstOrDefault(e => e.EventId == eventId);
        }

        public List<Event> GetAllEvents()
        {
            return events.ToList();
        }
    }
}