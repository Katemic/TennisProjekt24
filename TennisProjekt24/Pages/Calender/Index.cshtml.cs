using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TennisProjekt24.Pages.Calender
{
    public class IndexModel : PageModel
    {
        public int CurrentYear { get; set; }
        public int CurrentMonth { get; set; }
        public void OnGet()
        {
        }
    }
}
