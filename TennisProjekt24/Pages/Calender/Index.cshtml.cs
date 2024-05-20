using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Calender
{
    public class IndexModel : PageModel
    {
        private IEventService _eventService;
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        [BindProperty]
        //[BindProperty(SupportsGet = true)]
        public int CurrentMonth { get; set; }
        [BindProperty]
        public DateTime CurrentDay { get; set; }
        public DateTime FirstDayOfMonth { get; set; }
        public DateTime FirstDay { get; set; }
        [BindProperty]
        public List<Event> Events { get; set; }

        public IndexModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        public void OnGet(int currentMonth)
        {
            CurrentMonth = currentMonth;
            //CurrentYear = DateTime.Now.Year;
            //CurrentMonth = DateTime.Now.Month;
            FirstDayOfMonth = new DateTime(CurrentYear, CurrentMonth, 1);
            FirstDay = FirstDayOfMonth.AddDays(-(int)FirstDayOfMonth.DayOfWeek);
            Events = _eventService.GetAllEvents();
        }
        public void OnPostNext()
        {
            CurrentMonth = CurrentMonth + 1;
            if (CurrentMonth == 13) 
            {
                CurrentMonth = 1;
                CurrentYear++;
            }
        }
    }
}
