using System.Collections.Generic;
using Community_Events_Platform.Models;

namespace Community_Events_Platform.Interfaces
{
    public interface ICommentService
    {
        Comment AddComment(int eventId, int userId, string text);
        bool EditComment(int commentId, int userId, string newText);
        List<Comment> GetCommentsForEvent(int eventId);
    }
}