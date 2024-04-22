using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class AddMemberModel : PageModel
    {
        
        private IMemberService _memberService;

        [BindProperty]
        public Member NewMember { get; set; }

        public AddMemberModel(IMemberService memberService)
        {
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

            _memberService.AddMember(NewMember);
            return RedirectToPage("Index");

        }

    }
}
