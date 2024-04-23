using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class LogInModel : PageModel
    {

        private IMemberService _memberService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public LogInModel(IMemberService memberService)
        {
            _memberService = memberService;
        }


        public void OnGet()
        {
        }


        public IActionResult OnPost() 
        {
            bool validInput = true;
            if (Username == null)
            {
                validInput = false;
                Message = "Udfyld brugernavn";
            }
            if (Password == null) 
            {
                if (validInput == false) 
                {
                    Message = "Udfyld brugernavn og password";
                }
                else 
                {
                    validInput = false;
                    Message = "Udfyld password";
                }
            }

            if (validInput) 
            {
                Member memberLogin = _memberService.VerifyLogin(Username, Password);
                if (memberLogin != null) 
                {
                    HttpContext.Session.SetInt32("MemberId", memberLogin.MemberId);
                    return RedirectToPage("Profile");
                }
                else
                {
                    Message = "Forkert brugernavn eller password";
                    Username = "";
                    Password = "";
                    return Page();
                }
            }
            else
            {
                return Page();
            }

        }


    }
}
