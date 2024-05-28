using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Helpers;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Calender
{
    public class IndexModel : PageModel
    {
        private IEventService _eventService;
        [BindProperty(SupportsGet = true)]
        public int CurrentYear { get; set; }
        [BindProperty(SupportsGet = true)]
        public Months Months { get; set; }
        public int CurrentMonth { get; set; }
        public DateTime FirstDayOfMonth { get; set; }
        public DateTime FirstDay { get; set; }
        [BindProperty]
        public List<Event> Events { get; set; }
        public string NameofMonth { get; set; }

        public IndexModel(IEventService eventService)
        {
            _eventService = eventService;
        }
        public void OnGet()
        {
            try
            {
                if (Months == 0)
                {
                    CurrentMonth = DateTime.Now.Month;
                }
                else
                {
                    CurrentMonth = (int)Months;
                }
                NameofMonth = ((Months)CurrentMonth).ToString();
                if (CurrentYear == 0)
                {
                    CurrentYear = DateTime.Now.Year;
                }
                FirstDayOfMonth = new DateTime(CurrentYear, CurrentMonth, 1);
                FirstDay = FirstDayOfMonth.AddDays(-(int)FirstDayOfMonth.DayOfWeek + 1);
                if (FirstDayOfMonth.DayOfWeek == DayOfWeek.Sunday)
                {
                    FirstDay = FirstDayOfMonth.AddDays(-(int)FirstDayOfMonth.DayOfWeek - 6);
                }
                Events = _eventService.GetAllEvents();
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
        }
    }
}
