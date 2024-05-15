using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Practices
{
    public class DeletePracticeModel : PageModel
    {
        private IPracticeService _service;
        public Practice DeletePractice { get; set; }
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("/Members/LogIn");
            }
            else
            {
                DeletePractice = _service.GetPractice(id);
                return Page();
            }
        }

        public DeletePracticeModel(IPracticeService prac)
        {
            _service = prac;
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeletePractice(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }

    }
}
