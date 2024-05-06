using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Pages.Events
{
    public class AddEventModel : PageModel
    {
        private IEventService _eventService;
        [BindProperty]
        public Event NewEvent { get; set; }
        public AddEventModel(IEventService eventService)
        {
            _eventService = eventService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            _eventService.AddEvent(NewEvent);
            return RedirectToPage("Index");
        }
    }
}
