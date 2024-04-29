using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Events
{
    public class UpdateEventModel : PageModel
    {
        private IEventService _eventService;
        [BindProperty]
        public Event EventUpdate { get; set; }
        public UpdateEventModel(IEventService eventService)
        {
            _eventService = eventService;
        }
        public void OnGet(int eventId)
        {
            EventUpdate = _eventService.GetEvent(eventId);
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _eventService.UpdateEvent(EventUpdate, EventUpdate.EventId);
            return RedirectToPage("Index");
        }
    }
}