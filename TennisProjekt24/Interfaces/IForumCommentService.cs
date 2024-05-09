using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IForumCommentService
    {
        public bool CreateComment(ForumComment forumComment);
        public bool DeleteComment(int commentId);
        public bool UpdateComment(ForumComment forumComment, int commentId);
        public List<ForumComment> GetPostComments(int postId);
        public List<ForumComment> GetPostCommentsDesc(int postId);
        public ForumComment GetComment(int commentId);
    }
}
