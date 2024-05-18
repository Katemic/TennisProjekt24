using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class AddMemberModel : PageModel
    {
        
        private IMemberService _memberService;

        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Member NewMember { get; set; }

        public string UsernameMessage { get; set; }

        [BindProperty]
        //[Required (ErrorMessage = "Tilføj billede")]
        public IFormFile? Photo { get; set; }

        
        


        public AddMemberModel(IMemberService memberService, IWebHostEnvironment webHostEnvironment)
        {
            _memberService = memberService;
            _webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {

        }


        public IActionResult OnPost() 
        {

            

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_memberService.CheckUsername(NewMember.Username) == false)
            {
                UsernameMessage = "Brugernavnet er allerede taget, vælg venligst et andet";
                return Page();
            }

            if (Photo == null)
            {
                NewMember.Image = "default.jpg";
            }

            if (Photo != null)
            {
                if (NewMember.Image != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images/memberimages", NewMember.Image);
                    System.IO.File.Delete(filePath);
                }

                NewMember.Image = ProcessUploadedFile();
            }

            _memberService.AddMember(NewMember);

            return RedirectToPage("Login");

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
