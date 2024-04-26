using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Courts
{
    public class CreateCourtsModel : PageModel
    {
        private ICourtService _courtService;

        [BindProperty]
        public Court NewCourt { get; set; }

        public CreateCourtsModel(ICourtService courtService)
        {
            _courtService = courtService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _courtService.AddCourt(NewCourt);
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
