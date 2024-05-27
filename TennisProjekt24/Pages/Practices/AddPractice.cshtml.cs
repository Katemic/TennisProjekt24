using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
        public int InstructorId { get; set; }
        public IActionResult OnGet()
        {
            try {
                if (HttpContext.Session.GetInt32("MemberId") == null)
                {
                    return RedirectToPage("/Members/LogIn");
                }
                else
                {
                    Instructors = SetSelectlist();
                    return Page();
                }
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;

            }
            Instructors = SetSelectlist();
            return Page();
            
        }

        public AddPracticeModel(IPracticeService serv, IInstructorService instructorService)
        {
            _service = serv;
            _instructorService = instructorService;
            Instructors = SetSelectlist();
        }


        public IActionResult OnPost()
        {
            Practice.Instructor = _instructorService.GetInstructor(InstructorId);
            try
            {
                if (!ModelState.IsValid)
                {

                    Instructors = SetSelectlist();
                    return Page();
                }
                _service.AddPractice(Practice);
                return RedirectToPage("Index");
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;

            }
            return Page();
           
        }

        private List<SelectListItem> SetSelectlist() {
            List<Instructor> list = _instructorService.GetAllInstructors();
            return list.Select(x => new SelectListItem { Text = x.Name, Value = x.InstructorId.ToString() }).ToList();
        }

    }
}

