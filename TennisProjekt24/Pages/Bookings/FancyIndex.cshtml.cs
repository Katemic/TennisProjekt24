using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Bookings
{
    public class FancyIndexModel : PageModel
    {

        private IBookingService _bookingService;

        private ICourtService _courtService;

        [BindProperty]
        public DateOnly Date {  get; set; }

        public List<Court> Courts { get; set; } 

        public List<Booking> Bookings { get; set; }


        public FancyIndexModel(IBookingService bookingService, ICourtService courtService)
        {
            _bookingService = bookingService;
            _courtService = courtService;
        }


        public void OnGet()
        {
            Courts = _courtService.GetAllCourts();
            Bookings = _bookingService.GetAllBookings();

        }
    }
}
