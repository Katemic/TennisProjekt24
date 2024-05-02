using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services
{
    public class ForumCommentService : Connection, IForumCommentService
    {
        public bool CreateComment(int postId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public List<ForumComment> GetPostComments(int postId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateComment(ForumComment forumComment, int commentId)
        {
            throw new NotImplementedException();
        }
    }
}
