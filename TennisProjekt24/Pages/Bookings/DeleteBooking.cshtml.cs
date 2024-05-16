using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Bookings
{
    public class DeleteBookingModel : PageModel
    {

        private IBookingService _bookingService;


        public Booking BookingToDelete { get; set; }

        public DeleteBookingModel(IBookingService bookingService)
        {
             _bookingService = bookingService;
        }


        public void OnGet(int id)
        {
            BookingToDelete = _bookingService.GetBooking(id);
        }

        public IActionResult OnPost(int id) 
        {
            _bookingService.DeleteBooking(id);
            return RedirectToPage("BookingsMember");
        }


    }
}
