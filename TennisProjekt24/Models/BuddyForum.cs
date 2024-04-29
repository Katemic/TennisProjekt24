﻿namespace TennisProjekt24.Models
{
    public class BuddyForum
    {
        public int PostId { get; set; }
        public DateTime DateTime { get; set; }
        public int MemberId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public BuddyForum(int postId, DateTime dateTime, int memberId, string title, string text)
        {
            PostId = postId;
            DateTime = dateTime;
            MemberId = memberId;
            Title = title;
            Text = text;
        }
    }
}
