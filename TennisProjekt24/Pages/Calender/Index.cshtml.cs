using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TennisProjekt24.Pages.Calender
{
    public class IndexModel : PageModel
    {
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public int CurrentMonth { get; set; } = DateTime.Now.Month;
        public DateTime CurrentDay { get; set; }
        public void OnGet()
        {
        }
    }
}
