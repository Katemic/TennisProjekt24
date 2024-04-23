using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Courts
{
    public class DeleteCourtModel : PageModel
    {
        private ICourtService _courtService;
        public Court DeleteCourt { get; set; }

        public DeleteCourtModel(ICourtService courtService)
        {
            _courtService = courtService;
        }

        public IActionResult OnGet(int courtId)
        {
            try
            {
                DeleteCourt = _courtService.GetCourt(courtId);
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
        public IActionResult OnPost(int courtId)
        {
            try
            {
                _courtService.DeleteCourt(courtId);
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("Index");
        }
    }
}
