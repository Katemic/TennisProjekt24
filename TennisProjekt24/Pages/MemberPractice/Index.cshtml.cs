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
        public List<Member> Members { get; set; }
        [BindProperty]
        public List<(int, int)> MembersPractices { get; set; }
        [BindProperty]
        public var View = from 
        public void OnGet(int practiceId)
        {
            Practice = _practiceService.GetPractice(practiceId);
            Members = _memberService.GetAllMembers();
            MembersPractices = _practiceService.GetAllMemberPracticesByPractice(practiceId);

        }
        public IndexModel(IPracticeService serv, IMemberService memberService)
        {
            _practiceService = serv;
            _memberService = memberService;
        }
    }
}
