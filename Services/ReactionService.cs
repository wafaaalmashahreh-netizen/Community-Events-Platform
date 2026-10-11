using Community_Events_Platform.Interfaces;
using Community_Events_Platform.Models;
using System.Collections.Generic;
using System.Linq;

namespace Community_Events_Platform.Services
{
    public class ReactionService : IReactionService
    {
        private readonly List<EventReaction> reactions = new();

        public void React(int eventId, int userId, ReactionType type)
        {
            var existing = reactions.FirstOrDefault(r => r.EventId == eventId && r.UserId == userId);

            if (existing != null)
                existing.ChangeType(type);
            else
                reactions.Add(new EventReaction(eventId, userId, type));
        }

        public int CountReactions(int eventId, ReactionType type)
        {
            return reactions.Count(r => r.EventId == eventId && r.Type == type);
        }
    }
}