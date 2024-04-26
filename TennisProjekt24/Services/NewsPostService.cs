using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services
{
    public class NewsPostService : Connection, INewsPostService
    {

        private string _addPostSQL = "INSERT INTO NewsPosts VALUES (@Title, @Text, @Date, @MemberId)";
        private string _deletePostSQL = "DELETE FROM NewsPosts WHERE NewsPostId = @Id";
        private string _getAllPostsSQL = "SELECT NewsPostId, Title, Text, Date, MemberId FROM NewsPosts";
        private string _getPostSQL = "SELECT NewsPostId, Title, Text, Date, MemberId FROM NewsPosts WHERE NewsPostId = @Id";
        private string _updatePostSQL = "UPDATE NewsPosts SET Title = @Title, Text = @Text, Date = @Date, MemberId = @MemberId WHERE NewsPostId = @Id";


        public bool AddPost(NewsPost newsPost)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(int newsPostId)
        {
            throw new NotImplementedException();
        }

        public List<NewsPost> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public NewsPost GetPost(int newsPostId)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePost(NewsPost newsPost, int newsPostId)
        {
            throw new NotImplementedException();
        }
    }
}
