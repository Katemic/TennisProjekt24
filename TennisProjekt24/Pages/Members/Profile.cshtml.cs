using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class ProfileModel : PageModel
    {

        private IMemberService _memberService;

        public Member CurrentMember { get; set; }

        public ProfileModel(IMemberService memberService)
        {
            _memberService = memberService;
        }


        public IActionResult OnGet()
        {
            int sessionMemberId = (int) HttpContext.Session.GetInt32("MemberId");
            if (sessionMemberId == null) 
            {
                return RedirectToPage("LogIn");
            }
            else
            {
                CurrentMember = _memberService.GetMember(sessionMemberId);
                return Page();
            }
        }
    }
}
