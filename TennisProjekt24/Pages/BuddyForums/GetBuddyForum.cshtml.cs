using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Pages.BuddyForums
{
    public class GetBuddyForumModel : PageModel
    {
        private IBuddyForumService _buddyForumService;
        private IMemberService _memberService;
        private IForumCommentService _forumCommentService;
        public BuddyForum GetBuddyForum { get; set; }
        [BindProperty]
        public ForumComment CreateComment { get; set; }
        public List<ForumComment> ForumComments { get; set; }
        public bool Descending { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrderAscDesc { get; set; }
        [BindProperty(SupportsGet = true)]
        public int postId { get; set; }

        public GetBuddyForumModel(IBuddyForumService buddyForumService, IMemberService memberService, IForumCommentService forumCommentService)
        {
            _buddyForumService = buddyForumService;
            _memberService = memberService;
            _forumCommentService = forumCommentService;
        }

        public IActionResult OnGet(int postId)
        {
            try
            {
                this.postId = postId;
                GetBuddyForum = _buddyForumService.GetPostById(postId);
                ForumComments = _forumCommentService.GetPostComments(postId);
                if (SortOrderAscDesc == "Ascending")
                {
                    ForumComments.Reverse();
                }
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            //return RedirectToPage("/BuddyForums/GetBuddyForum", new { postId = postId });
            return Page();
        }
        public IActionResult OnPost(int postId)
        {
            try
            {
                CreateComment.Commenter = _memberService.GetMember((int)HttpContext.Session.GetInt32("MemberId"));
                CreateComment.DateTime = DateTime.Now;
                CreateComment.PostId = postId;
                _forumCommentService.CreateComment(CreateComment);
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
