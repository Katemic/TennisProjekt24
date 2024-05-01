using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.BuddyForums
{
    public class GetBuddyForumModel : PageModel
    {
        private IBuddyForumService _buddyForumService;
        public BuddyForum GetBuddyForum { get; set; }

        public GetBuddyForumModel(IBuddyForumService buddyForumService)
        {
            _buddyForumService = buddyForumService;
        }

        public IActionResult OnGet(int postId)
        {
            try
            {
                GetBuddyForum = _buddyForumService.GetPostById(postId);
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
    }
}
