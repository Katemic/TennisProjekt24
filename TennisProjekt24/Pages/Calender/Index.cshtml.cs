using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Helpers;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Calender
{
    public class IndexModel : PageModel
    {
        private IEventService _eventService;
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        [BindProperty(SupportsGet = true)]
        public Months Months { get; set; }
        public int CurrentMonth { get; set; }
        public DateTime FirstDayOfMonth { get; set; }
        public DateTime FirstDay { get; set; }
        [BindProperty]
        public List<Event> Events { get; set; }

        public IndexModel(IEventService eventService)
        {
            _eventService = eventService;
        }
        public void OnGet()
        {
            if (Months == 0)
            {
                CurrentMonth = DateTime.Now.Month;
            } else
            {
                CurrentMonth = (int)Months;
            }
            FirstDayOfMonth = new DateTime(CurrentYear, CurrentMonth, 1);
            FirstDay = FirstDayOfMonth.AddDays(-(int)FirstDayOfMonth.DayOfWeek);
            if (FirstDayOfMonth.DayOfWeek == DayOfWeek.Sunday)
            {
                FirstDay = FirstDayOfMonth.AddDays(-(int)FirstDayOfMonth.DayOfWeek - 7);
            }
            Events = _eventService.GetAllEvents();
        }
    }
}
