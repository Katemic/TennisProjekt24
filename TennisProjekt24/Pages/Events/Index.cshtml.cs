using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Events
{
    public class IndexModel : PageModel
    {
        private IEventService _eventService;
        public List<Event> events { get; set; }
        public IndexModel(IEventService eventService)
        {
            _eventService = eventService;
        }
        public void OnGet()
        {
            events = _eventService.GetAllEvents();
        }
    }
}
