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
        public List<SelectListItem> Instructors { get; set; }

        public void OnGet(int id)
        {
            Practice = _service.GetPractice(id);
            Instructors = _instructorService.GetAllInstructors().Select(i =>
                                           new SelectListItem
                                           {
                                               Value = i.InstructorId.ToString(),
                                               Text = i.Name
                                           }).ToList();
        }

        public UpdatePracticeModel(IPracticeService serv, IInstructorService instructorService)
        {
            _service = serv;
            _instructorService = instructorService;
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
