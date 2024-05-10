using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Practices
{
    public class IndexModel : PageModel
    {
        public List<Practice> Practices { get; set; }
        IPracticeService practiceService { get; set; }
        public void OnGet()
        {
            Practices = practiceService.GetAllPractices(null);
        }
        public IndexModel(IPracticeService prac)
        {
            practiceService = prac;
        }
    }
}
