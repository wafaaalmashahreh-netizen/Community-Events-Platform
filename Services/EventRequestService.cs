using System.Collections.Generic;
using System.Linq;
using Community_Events_Platform.Interfaces;
using Community_Events_Platform.Models;

namespace Community_Events_Platform.Services
{
    public class EventRequestService : IEventRequestService
    {
        private readonly List<EventRequest> requests = new();
        private int nextId = 1;

        public EventRequest AddRequest(int userId, string text)
        {
            var request = new EventRequest(userId, nextId++, text);
            requests.Add(request);
            return request;
        }

        public List<EventRequest> GetAllRequests()
        {
            return requests
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }
    }
}