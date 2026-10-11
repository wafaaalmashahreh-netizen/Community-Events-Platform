using System.Collections.Generic;
using System.Linq;
using Community_Events_Platform.Interfaces;
using Community_Events_Platform.Models;

namespace Community_Events_Platform.Services
{
    public class CommentService : ICommentService
    {
        private readonly List<Comment> comments = new();
        private int nextId = 1;

        public Comment AddComment(int eventId, int userId, string text)
        {
            var comment = new Comment(userId, nextId++, text, eventId);
            comments.Add(comment);
            return comment;
        }

        public bool EditComment(int commentId, int userId, string newText)
        {
            var comment = comments.FirstOrDefault(c => c.CommentId == commentId);

            if (comment == null || comment.UserId != userId)
                return false;

            comment.Edit(newText);
            return true;
        }

        public List<Comment> GetCommentsForEvent(int eventId)
        {
            return comments
                .Where(c => c.EventId == eventId)
                .OrderBy(c => c.CreatedAt)
                .ToList();
        }
    }
}