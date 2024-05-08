using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.ForumComments
{
    public class DeleteCommentModel : PageModel
    {
        private IForumCommentService _commentService;
        [BindProperty]
        public ForumComment DeleteComment { get; set; }

        public DeleteCommentModel(IForumCommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult OnGet(int commentId)
        {
            try
            {
                DeleteComment = _commentService.GetComment(commentId);
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
        public IActionResult OnPost(int commentId)
        {
            try
            {
                _commentService.DeleteComment(commentId);
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("/BuddyForums/GetBuddyForum", new { postId = DeleteComment.PostId });
        }
    }
}
