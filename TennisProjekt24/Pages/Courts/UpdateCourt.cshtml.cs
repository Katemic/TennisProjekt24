using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Courts
{
    public class UpdateCourtModel : PageModel
    {
        private ICourtService _courtService;

        [BindProperty]
        public Court CourtToUpdate { get; set; }

        public UpdateCourtModel(ICourtService courtService) 
        {
            _courtService = courtService;
        }
        public void OnGet(int courtId)
        {
            CourtToUpdate = _courtService.GetCourt(courtId);
        }

        public IActionResult OnPostUpdate()
        {
            try
            {
                _courtService.UpdateCourt(CourtToUpdate.CourtId, CourtToUpdate);
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

        public IActionResult OnPostDelete()
        {
            try
            {
                _courtService.DeleteCourt(CourtToUpdate.CourtId);
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
