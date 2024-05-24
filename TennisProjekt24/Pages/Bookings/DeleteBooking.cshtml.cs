using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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
            try
            {
                BookingToDelete = _bookingService.GetBooking(id);
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

        public IActionResult OnPost(int id) 
        {
            try
            {
                _bookingService.DeleteBooking(id);
                return RedirectToPage("BookingsMember");
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + sql.Message;

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Der er sket en fejl:   " + ex.Message;

            }
            return Page();
        }


    }
}
