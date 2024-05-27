using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Events
{
    public class DeleteEventModel : PageModel
    {
        private IEventService _eventService;
        [BindProperty]
        public Event EventDelete { get; set; }
        public DeleteEventModel(IEventService eventService)
        {
            _eventService = eventService;
        }
        public void OnGet(int eventId)
        {
            EventDelete = _eventService.GetEvent(eventId);
        }
        public IActionResult OnPost(int eventId)
        {
            try
            {
                _eventService.DeleteEvent(eventId);
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
    }
}
