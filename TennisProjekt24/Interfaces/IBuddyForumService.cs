using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IBuddyForumService
    {
        bool CreatePost(BuddyForum post);
        bool DeletePost(int postId);
        bool UpdatePost(int postId, BuddyForum post);
        BuddyForum GetPostById(int postId);
        List<BuddyForum> GetAllPosts();
        List<BuddyForum> GetBySkillPosts(int skill);
    }
}
