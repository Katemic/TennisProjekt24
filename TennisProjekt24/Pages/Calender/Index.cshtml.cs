using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TennisProjekt24.Pages.Calender
{
    public class IndexModel : PageModel
    {
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public int CurrentMonth { get; set; } = DateTime.Now.Month;
        [BindProperty]
        public DateOnly CurrentDay { get; set; } = DateOnly.MinValue;
        public void OnGet()
        {
        }
    }
}
