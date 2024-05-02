using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.BuddyForums
{
    public class CreateBuddyForumModel : PageModel
    {
        private IBuddyForumService _buddyForumService;
        private IMemberService _memberService;

        [BindProperty]
        public BuddyForum NewBuddyForum { get; set; }

        public CreateBuddyForumModel(IBuddyForumService buddyForumService, IMemberService memberService)
        {
            _buddyForumService = buddyForumService;
            _memberService = memberService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                NewBuddyForum.Poster = _memberService.GetMember((int)HttpContext.Session.GetInt32("MemberId"));
                NewBuddyForum.DateTime = DateTime.Now;
                _buddyForumService.CreatePost(NewBuddyForum);
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
