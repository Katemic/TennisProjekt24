using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("LogIn");
            }
            else
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
                BookingsList = _bookingService.GetBookingsByMember(CurrentMember.MemberId);
                return Page();
            }
        }
    }
}
