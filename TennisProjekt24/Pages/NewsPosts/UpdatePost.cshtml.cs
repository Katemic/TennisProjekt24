using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.NewsPosts
{
    public class UpdatePostModel : PageModel
    {

        private INewsPostService _newsPostService;


        [BindProperty]
        public NewsPost UpdatedPost { get; set; }

        public string Message { get; set; }

        public UpdatePostModel(INewsPostService newsPostService)
        {
             _newsPostService = newsPostService;
        }

        public void OnGet(int id)
        {
            try
            {
                UpdatedPost = _newsPostService.GetPost(id);
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


                //if (!ModelState.IsValid) 
                //{
                //    return Page();
                //}

                if (UpdatedPost.Title == null)
                {
                    Message = "Du skal tilføje en titel";
                    return Page();
                }
                if (UpdatedPost.Text == null)
                {
                    Message = "Du skal tilføje tekst";
                    return Page();
                }

                _newsPostService.UpdatePost(UpdatedPost, UpdatedPost.NewsPostId);
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
