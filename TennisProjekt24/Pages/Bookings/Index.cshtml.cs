using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Bookings
{
    public class IndexModel : PageModel
    {

        private IBookingService _bookingService;

        public List<Booking> BookingsList { get; set; }

        public IndexModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }


        public void OnGet()
        {
            BookingsList = _bookingService.GetAllBookings();
        }
    }
}
