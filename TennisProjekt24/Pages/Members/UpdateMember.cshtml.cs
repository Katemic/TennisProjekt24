using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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

        public Member CurrentMember { get; set; }

        public UpdateMemberModel(IMemberService memberService)
        {
            _memberService = memberService;
        }


        public void OnGet(int id)
        {
            try
            {
                UpdatedMember = _memberService.GetMember(id);
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
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

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _memberService.UpdateMember(UpdatedMember, UpdatedMember.MemberId);
                return RedirectToPage("Profile");

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
