using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Instructors
{
    public class DeleteInstructorModel : PageModel
    {
        private IInstructorService _service;
        public Instructor DeleteInstructor { get; set; }
        public IActionResult OnGet(int id)
        {
            DeleteInstructor = _service.GetInstructor(id);
            return Page();
        }

        public DeleteInstructorModel(IInstructorService serv)
        {
            _service = serv;
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteInstructor(id);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }
    }
}
