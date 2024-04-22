using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {

        private IMemberService _memberService;


        public Member MemberToDelete { get; set; }

        public DeleteMemberModel(IMemberService memberService)
        {
            _memberService = memberService;
        }


        public void OnGet(int id)
        {
            MemberToDelete = _memberService.GetMember(id);
        }

        public IActionResult OnPost(int id) 
        {
            _memberService.DeleteMember(id);
            return RedirectToPage("Index");
        }


    }
}
