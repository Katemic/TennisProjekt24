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

        public string UsernameMessage { get; set; }

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
             if (_memberService.CheckUsername(NewMember.Username) == false)  
            {
                UsernameMessage = "Brugernavnet er allerede taget, vælg venligst et andet";
                return Page();
            }

            _memberService.AddMember(NewMember);
            return RedirectToPage("Index");

        }

    }
}
