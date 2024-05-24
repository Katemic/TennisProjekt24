using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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


        public IActionResult OnGet()
        {
            try
            {

                if (HttpContext.Session.GetInt32("MemberId") != null)
                {
                    return RedirectToPage("Profile");
                }

                return Page();

            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + sql.Message;

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + ex.Message;

            }
            return Page();

        }

        public void OnGetLogout()
        {
            try
            {
                HttpContext.Session.Remove("MemberId");
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + sql.Message;

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + ex.Message;

            }

        }


        public IActionResult OnPost() 
        {

            try
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
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + sql.Message;

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + ex.Message;

            }

            return Page();

        }


    }
}
