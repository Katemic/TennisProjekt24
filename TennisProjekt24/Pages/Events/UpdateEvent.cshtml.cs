using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Events
{
    public class UpdateEventModel : PageModel
    {
        private IEventService _eventService;
        private IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public Event EventUpdate { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Tilf�j billede")]
        public IFormFile Picture { get; set; }
        public string Message { get; set; }

        public UpdateEventModel(IEventService eventService, IWebHostEnvironment webHostEnvironment)
        {
            _eventService = eventService;
            _webHostEnvironment = webHostEnvironment;
        }
        public void OnGet(int eventId)
        {
            EventUpdate = _eventService.GetEvent(eventId);
        }
        public IActionResult OnPost()
        {
            if (EventUpdate.Date < DateTime.Now)
            {
                Message = "V�lg dato";

                return Page();
            }
            if (EventUpdate.Title == null)
            {
                Message = "Tilf�j titel";

                return Page();
            }
            if (EventUpdate.Description == null)
            {
                Message = "Tilf�j beskrivelse";

                return Page();
            }
            if (EventUpdate.Place == null)
            {
                Message = "Tilf�j Afholdelsessted";

                return Page();
            }
            try
            {
                _eventService.UpdateEvent(EventUpdate.EventId, EventUpdate);
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