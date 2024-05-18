using Azure.Core.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Contracts;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class UpdatePasswordModel : PageModel
    {

        private IMemberService _memberService;

        [BindProperty]
        public Member UpdatedMember { get; set; }

        public string Message { get; set; }

        [BindProperty]
        public string OldPassword { get; set; }
        [BindProperty]
        public string NewPassword { get; set; }

        public UpdatePasswordModel(IMemberService memberService)
        {
             _memberService = memberService;
        }

        public void OnGet(int id)
        {
            UpdatedMember = _memberService.GetMember(id);

        }


        public IActionResult OnPost(int id) 
        {

            UpdatedMember = _memberService.GetMember(id);
            if (UpdatedMember.Password != OldPassword) 
            {
                Message = "Du har indtastet det forkerte password";
                return Page();
            }

            if (NewPassword == null)
            {
                Message = "udfyld dit nye password";
                return Page();  
            }


            _memberService.UpdatePassword(id, NewPassword);
            return RedirectToPage("Profile");

        }

    }
}
