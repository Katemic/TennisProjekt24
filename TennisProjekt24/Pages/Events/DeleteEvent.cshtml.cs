using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            _eventService.DeleteEvent(eventId);
            return RedirectToPage("Index");
        }
    }
}
