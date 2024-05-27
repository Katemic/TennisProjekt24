using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Pages.Events
{
    public class AddEventModel : PageModel
    {
        private IEventService _eventService;
        private IMemberService _memberService;
        private IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public Event NewEvent { get; set; }
        public Member CurrentMember { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Tilføj billede")]
        public IFormFile Picture { get; set; }
        public string Message { get; set; }

        public AddEventModel(IEventService eventService, IMemberService memberService, IWebHostEnvironment webHostEnvironment)
        {
            _eventService = eventService;
            _memberService = memberService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("/Members/LogIn");
            }
            else
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            if (Picture != null)
            {
                if (NewEvent.Image != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images/eventImages", NewEvent.Image);
                    System.IO.File.Delete(filePath);
                }

                NewEvent.Image = ProcessUploadedFile();
            }
            if (NewEvent.Date < DateTime.Now)
            {
                Message = "Vælg datoo";

                return Page();
            }
            if (NewEvent.Title == null)
            {
                Message = "Tilføj titel";

                return Page();
            }
            if (NewEvent.Description == null)
            {
                Message = "Tilføj beskrivelse";

                return Page();
            }
            if (NewEvent.Place == null)
            {
                Message = "Tilføj Afholdelsessted";

                return Page();
            }
            if (NewEvent.Image == null)
            {
                Message = "Tilføj billede";

                return Page();
            }
            int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
            CurrentMember = _memberService.GetMember(sessionMemberId);
            NewEvent.Member = CurrentMember;
            try
            {
                _eventService.AddEvent(NewEvent);
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("Index");
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Picture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/eventImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Picture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
