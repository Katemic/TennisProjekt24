using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Bookings
{
    public class AddBookingModel : PageModel
    {

        private IBookingService _bookingService;

        [BindProperty]
        public Booking NewBooking { get; set; }

        public string Message { get; set; }


        public AddBookingModel(IBookingService bookingService)
        {
              _bookingService = bookingService;
        }

        public void OnGet()
        {

        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            if (_bookingService.CheckAvailability(NewBooking.Court, NewBooking.Date, NewBooking.Time) == false ) 
            {
                Message = "Banen er allerede booket på valgte tidspunkt, vælg et andet tidspunkt og prøv igen";
                return Page();

            }

            _bookingService.AddBooking(NewBooking);
            return RedirectToPage("Index");

        }


    }
}
