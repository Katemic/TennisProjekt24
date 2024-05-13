using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.BuddyForums
{
    public class UpdateBuddyForumModel : PageModel
    {
        private IBuddyForumService _buddyForumService;

        [BindProperty]
        public BuddyForum PostToUpdate { get; set; }

        public UpdateBuddyForumModel(IBuddyForumService buddyForumService)
        {
            _buddyForumService = buddyForumService;
        }
        public void OnGet(int postId)
        {
            PostToUpdate = _buddyForumService.GetPostById(postId);
        }

        public IActionResult OnPostUpdate()
        {
            try
            {
                _buddyForumService.UpdatePost(PostToUpdate.PostId, PostToUpdate);
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete()
        {
            try
            {
                _buddyForumService.DeletePost(PostToUpdate.PostId);
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("Index");
        }
    }
}
