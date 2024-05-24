using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.NewsPosts
{
    public class DeletePostModel : PageModel
    {
        private INewsPostService _newsPostService;

        public NewsPost PostToDelete { get; set; }

        public DeletePostModel(INewsPostService newsPostService)
        {
            _newsPostService = newsPostService;
        }


        public void OnGet(int id)
        {
            try
            {
                PostToDelete = _newsPostService.GetPost(id);
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


        public IActionResult OnPost(int id) 
        {
            try
            {
                _newsPostService.DeletePost(id);
                return RedirectToPage("index");
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
