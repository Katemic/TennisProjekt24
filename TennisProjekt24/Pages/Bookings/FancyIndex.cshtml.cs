using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Helpers;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Bookings
{
    public class FancyIndexModel : PageModel
    {

        public IBookingService _bookingService;

        private ICourtService _courtService;

        private IMemberService _memberService;

        [BindProperty(SupportsGet = true)]
        public DateOnly Date {  get; set; }

        public Member CurrentMember { get; set; }

        public List<Court> Courts { get; set; } 

        public List<Booking> Bookings { get; set; }

        //public List<TimeOnly> TimeOnly = new List<TimeOnly> { new TimeOnly(8,00), new TimeOnly(9,00), new TimeOnly(10,00) };

        public List<TimeOnly> TimeOnly = new TimeOnlyGenerator().TimeOnlyList;




        public FancyIndexModel(IBookingService bookingService, ICourtService courtService, IMemberService memberService)
        {
            _bookingService = bookingService;
            _courtService = courtService;
            _memberService = memberService;
        }



        public void OnGet()
        {
             
            Courts = _courtService.GetAllCourts();

            if (Date.Equals(new DateOnly(1,1,1)))
            {
                Date = DateOnly.FromDateTime(DateTime.Now);
                Bookings = _bookingService.GetBookingsByDate(Date);
            }
            else 
            {
                Bookings = _bookingService.GetBookingsByDate(Date);
            }

            if (HttpContext.Session.GetInt32("MemberId") != null)
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
            }



            //Courts = _courtService.GetAllCourts();
            //Bookings = _bookingService.GetAllBookings();

        }
    }
}
