using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using TennisProjekt24.Interfaces;

namespace TennisProjekt24.Pages.Members
{
    public class UpdatePhotoModel : PageModel
    {

        private IMemberService _memberService; 

        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        [Required(ErrorMessage = "Tilføj billede")]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public string? Image {  get; set; }

        public int MemberId { get; set; }

        public UpdatePhotoModel(IMemberService memberService, IWebHostEnvironment webHostEnvironment)
        {
             _memberService = memberService;
            _webHostEnvironment = webHostEnvironment;
        }


        public void OnGet(int id)
        {
            try
            {
                MemberId = id;
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + sql.Message;

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + ex.Message;

            }

        }

        public IActionResult OnPost(int id) 
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                if (Photo != null)
                {
                    if (Image != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images/memberimages", Image);
                        System.IO.File.Delete(filePath);
                    }

                    Image = ProcessUploadedFile();
                }

                _memberService.UpdatePhoto(id, Image);
                return RedirectToPage("Profile");
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + sql.Message;

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + ex.Message;

            }
            return Page();
        }



        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/memberimages");
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
