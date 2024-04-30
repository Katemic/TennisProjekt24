using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Instructors
{
    public class UpdateInstructorModel : PageModel
    {
        private IInstructorService _service;
        [BindProperty]
        public Instructor Instructor { get; set; }
        public void OnGet(int id)
        {
            Instructor = _service.GetInstructor(id);
        }

        public UpdateInstructorModel(IInstructorService serv)
        {
            _service = serv;
        }
       


        public IActionResult OnPostUpdate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.UpdateInstructor(Instructor, Instructor.InstructorId);
            return RedirectToPage("Index");
        }
    }
}
