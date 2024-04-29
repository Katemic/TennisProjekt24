using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using static TennisProjekt24.Models.Practice;

namespace TennisProjekt24.Pages.Practices
{
    public class AddPracticeModel : PageModel
    {
        private IPracticeService _service;
        [BindProperty]
        public Practice Practice { get; set; }
        [BindProperty]
        public PracticeTypeEnum Type { get; set; }
        public void OnGet()
        {
        }

        public AddPracticeModel(IPracticeService serv)
        {
            _service = serv;
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.AddPractice(Practice);
            return RedirectToPage("Index");
        }
    }
}

