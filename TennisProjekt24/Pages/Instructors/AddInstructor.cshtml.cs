using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Instructors
{
    public class AddInstructorModel : PageModel
    {
        private IInstructorService _service;
        [BindProperty]
        public Instructor Instructor { get; set; }
        public void OnGet()
        {
        }

        public AddInstructorModel(IInstructorService serv)
        {
            _service = serv;
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.AddInstructor(Instructor);
            return RedirectToPage("Index");
        }
    }
}
