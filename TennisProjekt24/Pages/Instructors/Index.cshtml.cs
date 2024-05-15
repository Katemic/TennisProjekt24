using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        public List<Instructor> Instructors { get; set; }
        private IInstructorService _instructorService;
        private IMemberService _memberService;
        public Member User { get; set; }
        public IndexModel(IInstructorService service, IMemberService memberService)
        {
            _instructorService = service;
            _memberService = memberService;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("/Members/LogIn");
            }
            else
            {
                Instructors = _instructorService.GetAllInstructors();
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                User = _memberService.GetMember(sessionMemberId);
                return Page();
            }
        }
    }
}
