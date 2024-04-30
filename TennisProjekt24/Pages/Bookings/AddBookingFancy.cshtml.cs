using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Bookings
{
    public class AddBookingFancyModel : PageModel
    {
        private IBookingService _bookingService;

        [BindProperty]
        public Booking NewBooking { get; set; }

        public string Message { get; set; }

        [BindProperty]
        public DateOnly Date {  get; set; }

        [BindProperty] 
        public TimeOnly Time { get; set; }

        [BindProperty]
        public int CourtId { get; set; }

        [BindProperty]
        public int CurrentMember {  get; set; }


        public AddBookingFancyModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public void OnGet(int id, DateOnly date, TimeOnly time)
        {
            CourtId = id;
            Date = date;
            Time = time;

        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_bookingService.CheckAvailability(NewBooking.Court, NewBooking.Date, NewBooking.Time) == false)
            {
                Message = "Banen er allerede booket på valgte tidspunkt, vælg et andet tidspunkt og prøv igen";
                return Page();

            }

            _bookingService.AddBooking(NewBooking);
            return RedirectToPage("Index");

        }
    }
}
