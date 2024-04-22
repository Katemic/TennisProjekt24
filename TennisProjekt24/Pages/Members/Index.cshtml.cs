using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class IndexModel : PageModel
    {

        private IMemberService _memberService;

        public List<Member> members { get; set; }

        public IndexModel(IMemberService memberService)
        {
             _memberService = memberService;
        }


        public void OnGet()
        {
            members = _memberService.GetAllMembers();
        }
    }
}
