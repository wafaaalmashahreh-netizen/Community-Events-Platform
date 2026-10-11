using System;
using Community_Events_Platform.Models;

namespace Community_Events_Platform.Extensions
{
    public static class EventExtensions
    {
        // True if the event is not cancelled and has not passed yet.
        public static bool IsUpcoming(this Event evt)
        {
            return evt.Status != EventStatus.Cancelled && evt.StartDate >= DateTime.Today;
        }
    }
}