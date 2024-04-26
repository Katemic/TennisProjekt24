using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class UpdateMemberModel : PageModel
    {

        private IMemberService _memberService;

        [BindProperty]
        public Member UpdatedMember { get; set; }

        public string UsernameMessage { get; set; }

        public UpdateMemberModel(IMemberService memberService)
        {
            _memberService = memberService;
        }


        public void OnGet(int id)
        {
            UpdatedMember = _memberService.GetMember(id);
        }

        public IActionResult OnPost() 
        {
 
            //if (_memberService.CheckUsername(UpdatedMember.Username) == false)
            //{
            //    UsernameMessage = "Brugernavnet er allerede taget, v�lg venligst et andet";
            //    return Page();
            //}

            if (!ModelState.IsValid) 
            {
                return Page();
            }

            _memberService.UpdateMember(UpdatedMember,UpdatedMember.MemberId); //m�ske overf�re id med onpost eller er det fint?
            return RedirectToPage("Index");
        }


    }
}
