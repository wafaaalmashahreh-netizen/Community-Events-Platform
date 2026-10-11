using System;
using System.Collections.Generic;
using System.Text;

namespace Community_Events_Platform.Models
{
    public class EventReaction
    {
        public int EventId { get; }
        public int UserId { get; }
        public ReactionType Type { get; private set; }

        public EventReaction(int eventId, int userId, ReactionType type)
        {
            EventId = eventId;
            UserId = userId;
            Type = type;
        }

        public void ChangeType(ReactionType newType)
        {
            Type = newType;
        }
    }
}
