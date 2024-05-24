using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TennisProjekt24.Pages.NewsPosts
{
    public class AddPostModel : PageModel
    {

        private INewsPostService _newsPostService;
        private IMemberService _memberService;

        [BindProperty]
        public NewsPost NewPost { get; set; }

        public Member CurrentMember { get; set; }

        public string Message { get; set; }

        public AddPostModel(INewsPostService newsPostService, IMemberService memberService)
        {
            _newsPostService = newsPostService;
            _memberService = memberService;
        }


        public IActionResult OnGet()
        {
            try
            {
                if (HttpContext.Session.GetInt32("MemberId") == null)
                {
                    return RedirectToPage("/Members/LogIn");
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


        public IActionResult OnPost() 
        {
            try
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
                NewPost.Date = DateTime.Now;
                NewPost.Member = CurrentMember;

                if (NewPost.Title == null)
                {
                    Message = "Du skal tilføje en titel";
                    return Page();
                }
                if (NewPost.Text == null)
                {
                    Message = "Du skal tilføje tekst";
                    return Page();
                }


                //if(!ModelState.IsValid) 
                //{
                //    return Page();
                //}

                _newsPostService.AddPost(NewPost);
                return RedirectToPage("Index");
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
