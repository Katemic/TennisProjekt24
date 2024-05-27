using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.MemberPractice
{
    public class IndexModel : PageModel
    {
        private IPracticeService _practiceService;
        private IMemberService _memberService;
        [BindProperty]
        public Practice Practice { get; set; }

        [BindProperty]
        public Member CurrentMember { get; set; }
  
        public void OnGet(int id)
        {
            Practice = _practiceService.GetPractice(id);
            Practice.Members = _memberService.GetAllMembers(id);
            int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
            CurrentMember = _memberService.GetMember(sessionMemberId);

        }
        public IndexModel(IPracticeService serv, IMemberService memberService)
        {
            _practiceService = serv;
            _memberService = memberService;
            
        }

        public IActionResult OnPost(int practiceId)
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
                return RedirectToPage("/Members/LogIn");
            else
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                _practiceService.AddMemberPractice(sessionMemberId, practiceId);
                return RedirectToPage("/Practices/Index");
            }
        }
    }
}
