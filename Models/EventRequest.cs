using System;
using System.Collections.Generic;
using System.Text;

namespace Community_Events_Platform.Models
{
    public class EventRequest
    {
        public int Id { get; }
        public int UserId { get; }
        public string Text { get; private set; }
        public DateTime CreatedAt { get; }

        public EventRequest(int id, int userId, string text)
        {
            Id = id;
            UserId = userId;
            Text = text;
            CreatedAt = DateTime.Now;
        }
    }
}







