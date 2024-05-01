using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Bookings
{
    public class AddBookingModel : PageModel
    {

        private IBookingService _bookingService;

        private IMemberService _memberService;

        
        public Member CurrentMember { get; set; }

        public SelectList MemberList { get; set;}


        [BindProperty]
        public Booking NewBooking { get; set; }

        public string Message { get; set; }


        public AddBookingModel(IBookingService bookingService, IMemberService memberService)
        {
            _bookingService = bookingService;
            _memberService = memberService;

            List<Member> members = _memberService.GetAllMembers();
            MemberList = new SelectList(members);
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("/Members/LogIn");
            }
            else
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
                return Page();
            }


        }


        public IActionResult OnPost()
        {

            int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
            CurrentMember = _memberService.GetMember(sessionMemberId);


            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            if (_bookingService.CheckAvailability(NewBooking.Court, NewBooking.Date, NewBooking.Time) == false ) 
            {
                Message = "Banen er allerede booket på valgte tidspunkt, vælg et andet tidspunkt og prøv igen";
                return Page();

            }

            NewBooking.Member = CurrentMember;
            _bookingService.AddBooking(NewBooking);
            return RedirectToPage("Index");

        }


    }
}
