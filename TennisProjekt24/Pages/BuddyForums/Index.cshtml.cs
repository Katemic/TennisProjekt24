using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.BuddyForums
{
    public class IndexModel : PageModel
    {
        private IBuddyForumService _buddyForumService;

        public List<BuddyForum> buddyForums { get; set; }

        public IndexModel(IBuddyForumService buddyForumService)
        {
            _buddyForumService = buddyForumService;
        }

        public void OnGet()
        {
            try
            {
                buddyForums = _buddyForumService.GetAllPosts();
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                buddyForums = new List<BuddyForum>();
                ViewData["ErrorMessage"] = ex.Message;
            }
        }
    }
}