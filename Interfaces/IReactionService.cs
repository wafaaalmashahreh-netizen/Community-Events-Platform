using Community_Events_Platform.Models;

namespace Community_Events_Platform.Interfaces
{
    public interface IReactionService
    {
        void React(int eventId, int userId, ReactionType type);
        int CountReactions(int eventId, ReactionType type);
    }
}