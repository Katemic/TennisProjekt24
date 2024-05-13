namespace TennisProjekt24.Models
{
    public class ForumComment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public Member Commenter { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public ForumComment()
        {
            
        }
        public ForumComment(int commentId, int postId, Member commenter, string text, DateTime dateTime)
        {
            CommentId = commentId;
            PostId = postId;
            Commenter = commenter;
            Text = text;
            DateTime = dateTime;
        }
    }
}
