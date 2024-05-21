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
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("/Members/LogIn");
            }
            else
            {
                Instructor = _service.GetInstructor(id);
                return Page();
            }
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
