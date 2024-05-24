using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class ProfileModel : PageModel
    {

        private IMemberService _memberService;

        public Member CurrentMember { get; set; }

        public ProfileModel(IMemberService memberService)
        {
            _memberService = memberService;
        }


        public IActionResult OnGet()
        {

            try
            {
                if (HttpContext.Session.GetInt32("MemberId") == null)
                {
                    return RedirectToPage("LogIn");
                }
                else
                {
                    int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                    CurrentMember = _memberService.GetMember(sessionMemberId);
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
