using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Instructors
{
    public class AddInstructorModel : PageModel
    {
        private IInstructorService _service;
        private IWebHostEnvironment _environment;
        [BindProperty]
        public Instructor Instructor { get; set; }
        [BindProperty]
        public IFormFile? Photo { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("/Members/LogIn");
            }
            else
            {
                return Page();
            }
            }

        public AddInstructorModel(IInstructorService serv, IWebHostEnvironment environment)
        {
            _service = serv;
            _environment = environment;
        }


        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Photo != null)
            {
                if (Instructor.Image != null)
                {
                    string filePath = Path.Combine(_environment.WebRootPath, "/images/instructorimages", Instructor.Image);
                    System.IO.File.Delete(filePath);
                }
                Instructor.Image = ProcessUploadedFile();
            }else
            {
                Instructor.Image = "default.jpg";
            }
            _service.AddInstructor(Instructor);
            return RedirectToPage("Index");
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images/instructorimages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
