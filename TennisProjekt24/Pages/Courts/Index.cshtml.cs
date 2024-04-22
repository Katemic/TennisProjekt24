using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Courts
{
    public class IndexModel : PageModel
    {
        private ICourtService _courtService;

        public List<Court> Courts { get; set; }

        public IndexModel(ICourtService courtService)
        {
            _courtService = courtService;
        }

        public void OnGet()
        {
            try
            {
                Courts = _courtService.GetAllCourts();
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                Courts = new List<Court>();
                ViewData["ErrorMessage"] = ex.Message;
            }
        }
    }
}
