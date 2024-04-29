using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface INewsPostService
    {

        bool AddPost( NewsPost newsPost );

        bool DeletePost( int newsPostId );

        bool UpdatePost( NewsPost newsPost, int newsPostId );

        NewsPost GetPost(int newsPostId );

        List<NewsPost> GetAllPosts();



    }
}
