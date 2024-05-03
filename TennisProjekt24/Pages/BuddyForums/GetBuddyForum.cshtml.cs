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
        public BuddyForum GetBuddyForum { get; set; }
        public ForumComment CreateComment { get; set; }

        public GetBuddyForumModel(IBuddyForumService buddyForumService, IMemberService memberService)
        {
            _buddyForumService = buddyForumService;
            _memberService = memberService;
        }

        public IActionResult OnGet(int postId)
        {
            try
            {
                GetBuddyForum = _buddyForumService.GetPostById(postId);
                CreateComment.Commenter = _memberService.GetMember((int)HttpContext.Session.GetInt32("MemberId"));
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
