namespace TennisProjekt24.Models
{
    public class ForumComment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int MemberId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public ForumComment()
        {
            
        }
        public ForumComment(int commentId, int postId, int memberId, string text, DateTime dateTime)
        {
            CommentId = commentId;
            PostId = postId;
            MemberId = memberId;
            Text = text;
            DateTime = dateTime;
        }
    }
}
