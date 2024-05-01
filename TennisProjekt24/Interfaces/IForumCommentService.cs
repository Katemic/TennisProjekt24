using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IForumCommentService
    {
        public bool CreateComment();
        public bool DeleteComment(int commentId);
        public bool UpdateComment(ForumComment forumComment, int commentId);
        public List<ForumComment> GetForumComments();
    }
}
