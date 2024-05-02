using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Pages.Bookings
{
    public class AddBookingFancyModel : PageModel
    {
        private IBookingService _bookingService;

        private IMemberService _memberService;

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
        public Member CurrentMember {  get; set; }

        //bruges til selectlist

        [BindProperty]
        public List<SelectListItem> MemberList2 { get; set; }

        [BindProperty]
        public int SecondMemberId { get; set; }


        public AddBookingFancyModel(IBookingService bookingService, IMemberService memberService)
        {
            _bookingService = bookingService;
            _memberService = memberService;
        }

        public IActionResult OnGet(int id, DateOnly date, TimeOnly time)
        {
            

            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("/Members/LogIn");
            }
            else
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
                CourtId = id;
                Date = date;
                Time = time;
                List<Member> members = _memberService.GetAllMembers();

                MemberList2 = members.Select(x => new SelectListItem { Text = x.Name, Value = x.MemberId.ToString() }).ToList();




                return Page();
            }


        }


        public IActionResult OnPost(int courtId, DateOnly date, TimeOnly time)
        {

            int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
            CurrentMember = _memberService.GetMember(sessionMemberId);


            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewBooking.Date = date;
            NewBooking.Time = time;
            NewBooking.Court = courtId;
            NewBooking.SecondMemberFull = _memberService.GetMember(SecondMemberId);
            NewBooking.Member = CurrentMember;

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
