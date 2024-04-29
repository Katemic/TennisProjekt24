using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Practices
{
    public class UpdatePracticeModel : PageModel
    {
        private IPracticeService _service;
        [BindProperty]
        public Practice Practice { get; set; }
        [BindProperty]
        public PracticeTypeEnum Type { get; set; }
        public void OnGet(int id)
        {
            Practice = _service.GetPractice(id);
        }

        public UpdatePracticeModel(IPracticeService serv)
        {
            _service = serv;
        }


        public IActionResult OnPostUpdate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(_service.UpdatePractice(Practice, Practice.PracticeId))
                return RedirectToPage("Index");
            else
                return Page();
        }
    }
}
