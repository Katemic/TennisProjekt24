using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Bookings
{
    public class IndexModel : PageModel
    {

        public IBookingService _bookingService;

        public List<Booking> BookingsList { get; set; }

        public List<string> TypeChoice = new List<string> { "Alle", "Medlem", "Event", "Træning" };

        [BindProperty(SupportsGet = true)]
        public string TypeFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortByTime { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortByOld { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public IndexModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }


        public void OnGet()
        {

            try
            {

                string sql = "";
                if (TypeFilter == "Medlem")
                {
                    sql += "AND Type = 1 ";
                }
                if (TypeFilter == "Event")
                {
                    sql += "AND Type = 2 ";
                }
                if (TypeFilter == "Træning")
                {
                    sql += "AND Type = 3 ";
                }

                if (sql.Length == 0)
                {
                    BookingsList = _bookingService.GetAllBookings();
                }
                else
                {
                    BookingsList = _bookingService.GetAllBookings(sql);
                }

                if (!string.IsNullOrEmpty(FilterCriteria))
                {
                    BookingsList = BookingsList.FindAll(c => c.Member.Name.ToLower().Contains(FilterCriteria.ToLower()) || c.SecondMemberFull.Name.ToLower().Contains(FilterCriteria.ToLower())).ToList();
                }

                if (SortByTime == "Nyeste først" || SortByTime == null)
                {
                    BookingsList = BookingsList.OrderBy(c => c.Date).ThenBy(c => c.Time).ToList();
                }
                if (SortByTime == "Ældste først")
                {
                    BookingsList = BookingsList.OrderByDescending(c => c.Date).ThenBy(c => c.Time).ToList();
                }

                if (SortByOld == "skjul gamle" || SortByOld == null)
                {
                    BookingsList = BookingsList.Where(c => c.Date >= DateOnly.FromDateTime(DateTime.Now)).ToList();
                }
                if (SortByOld == "vis gamle")
                {
                    BookingsList = BookingsList.Where(c => c.Date <= DateOnly.FromDateTime(DateTime.Now)).ToList();
                }

            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + sql.Message;

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + ex.Message;

            }

        }
    }
}
