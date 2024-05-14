using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class IndexModel : PageModel
    {

        private IMemberService _memberService;

        public List<Member> members { get; set; }

        public Member CurrentMember { get; set; }

        public IndexModel(IMemberService memberService)
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
                    members = _memberService.GetAllMembers();
                    return Page();
                }
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
                
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                
            }
            return Page();


        }
    }
}
