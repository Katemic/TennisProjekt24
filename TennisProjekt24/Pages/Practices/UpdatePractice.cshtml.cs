using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Pages.Practices
{
    public class UpdatePracticeModel : PageModel
    {
        private IPracticeService _service;
        private IInstructorService _instructorService;
        [BindProperty]
        public Practice Practice { get; set; }
        [BindProperty]
        public PracticeTypeEnum Type { get; set; }
        [BindProperty]
        public List<SelectListItem> Instructors { get; set; }
        [BindProperty]
        public int InstructorId { get; set; }

        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("/Members/LogIn");
            }
            else
            {
                Practice = _service.GetPractice(id);
                Instructors = _instructorService.GetAllInstructors().Select(i =>
                                               new SelectListItem
                                               {
                                                   Value = i.InstructorId.ToString(),
                                                   Text = i.Name
                                               }).ToList();
                return Page();
            }
        }

        public UpdatePracticeModel(IPracticeService serv, IInstructorService instructorService)
        {
            _service = serv;
            _instructorService = instructorService;
        }


        public IActionResult OnPostUpdate()
        {
            Practice.Instructor = _instructorService.GetInstructor(InstructorId);
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (_service.UpdatePractice(Practice, Practice.PracticeId))
                return RedirectToPage("Index");
            else
                //return (Page(), new { Instructors = _instructorService.GetAllInstructors()} );
                return Page();

        }
    }
}
