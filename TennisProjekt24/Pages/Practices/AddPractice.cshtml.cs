using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;
using static TennisProjekt24.Models.Practice;

namespace TennisProjekt24.Pages.Practices
{
    public class AddPracticeModel : PageModel
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
        public int InstructorId {  get; set; } 
        public IActionResult OnGet(List<Instructor> instructors)
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("/Members/LogIn");
            }
            else
            {
                if (instructors == null || instructors.Count == 0)
                    Instructors = _instructorService.GetAllInstructors().Select(i =>
                                                    new SelectListItem
                                                    {
                                                        Value = i.InstructorId.ToString(),
                                                        Text = i.Name
                                                    }
                                                    ).ToList();
                else
                    Instructors = instructors.Select(i =>
                                                    new SelectListItem
                                                    {
                                                        Value = i.InstructorId.ToString(),
                                                        Text = i.Name
                                                    }
                                                    ).ToList(); ;
                return Page();
            }
        }

        public AddPracticeModel(IPracticeService serv, IInstructorService instructorService)
        {
            _service = serv;
            _instructorService = instructorService;
           
        }


        public IActionResult OnPost()
        {
            Practice.Instructor = _instructorService.GetInstructor(InstructorId);

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            _service.AddPractice(Practice);
            return RedirectToPage("Index");
        }
    }
}

