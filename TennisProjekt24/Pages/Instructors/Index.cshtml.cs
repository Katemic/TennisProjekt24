using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        public List<Instructor> Instructors { get; set; }
        private IInstructorService _instructorService;
        public IndexModel(IInstructorService service)
        {
            _instructorService = service;
        }
        public void OnGet()
        {
            Instructors = _instructorService.GetAllInstructors();
        }
    }
}
