using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using Microsoft.AspNetCore.Http;
using TennisProjekt24.Services;
using Microsoft.Data.SqlClient;

namespace TennisProjekt24.Pages.Practices
{
    public class IndexModel : PageModel
    {
        public List<Practice> Practices { get; set; }
        IPracticeService practiceService { get; set; }
        IMemberService memberService { get; set; }
        public Member User {  get; set; }
        public IActionResult OnGet()
        {
            //if (HttpContext.Session.GetInt32("MemberId") == null)
            //{
            //    return RedirectToPage("/Members/LogIn");
            //}
            try
            {
                if (HttpContext.Session.GetInt32("MemberId") != null)
                {
                    int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                    User = memberService.GetMember(sessionMemberId);
                }

                Practices = practiceService.GetAllPractices(null);
                foreach (var practice in Practices)
                {
                    practice.Members.AddRange(memberService.GetAllMembers(practice.PracticeId));
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
        public IndexModel(IPracticeService prac, IMemberService memberService)
        {
            practiceService = prac;
            this.memberService = memberService;
        }
    }
}
