using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Bookings
{
    public class BookingsMemberModel : PageModel
    {
        
        private IBookingService _bookingService;
        private IMemberService _memberService;

        public List<Booking> BookingsList { get; set; }  

        public Member CurrentMember { get; set; }


        public BookingsMemberModel(IBookingService bookingService, IMemberService memberService)
        {
            _bookingService = bookingService;
            _memberService = memberService;
        }



        public IActionResult OnGet()
        {
            try
            {
                if (HttpContext.Session.GetInt32("MemberId") == null)
                {
                    return RedirectToPage("LogIn");
                }
                else
                {
                    int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                    CurrentMember = _memberService.GetMember(sessionMemberId);
                    BookingsList = _bookingService.GetBookingsByMember(CurrentMember.MemberId).Where(c => c.Date >= DateOnly.FromDateTime(DateTime.Now)).OrderBy(c => c.Date).ThenBy(c => c.Time).ToList();
                    return Page();
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

            return Page();

        }
    }
}
