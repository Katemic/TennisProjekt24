using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.ForumComments
{
    public class UpdateCommentModel : PageModel
    {
        private IForumCommentService _commentService;

        [BindProperty]
        public ForumComment CommentToUpdate { get; set; }

        public UpdateCommentModel(IForumCommentService commentService)
        {
            _commentService = commentService;
        }
        public void OnGet(int commentId)
        {
            CommentToUpdate = _commentService.GetComment(commentId);
        }

        public IActionResult OnPostUpdate(int postId)
        {
            try
            {
                _commentService.UpdateComment(CommentToUpdate, CommentToUpdate.CommentId);
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("/BuddyForums/GetBuddyForum", new { postId = postId });
        }

        public IActionResult OnPostDelete(int postId)
        {
            try
            {
                _commentService.DeleteComment(CommentToUpdate.CommentId);
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("/BuddyForums/GetBuddyForum", new { postId = postId });
        }
    }
}