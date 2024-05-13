using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.BuddyForums
{
    public class DeleteBuddyForumModel : PageModel
    {
        private IBuddyForumService _buddyForumService;
        public BuddyForum DeleteBuddy { get; set; }

        public DeleteBuddyForumModel(IBuddyForumService buddyForumService)
        {
            _buddyForumService = buddyForumService;
        }

        public IActionResult OnGet(int postId)
        {
            try
            {
                DeleteBuddy = _buddyForumService.GetPostById(postId);
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
        public IActionResult OnPost(int postId)
        {
            try
            {
                _buddyForumService.DeletePost(postId);
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
