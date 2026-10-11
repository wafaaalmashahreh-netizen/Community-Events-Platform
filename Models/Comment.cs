using System;
using System.Collections.Generic;
using System.Text;

namespace Community_Events_Platform.Models
{
    public class Comment 
    {
        public  int  CommentId { get; }
        public string Text { get;  private set; }
        public int UserId { get; }
        public DateTime CreatedAt { get; }
        public int EventId { get; }
        public bool IsEdited { get; private set; }

        public Comment  (  int userid,int commintid, string text, int eventid)
        {
            CommentId = commintid;
            Text= text;
            UserId = userid;
            CreatedAt = DateTime.Now;
            EventId= eventid;
        }
       public void Edit(string newtext)
        {
            if (string.IsNullOrWhiteSpace(newtext))
                return;
            Text = newtext.Trim();
            IsEdited = true;
        }
    }
}
