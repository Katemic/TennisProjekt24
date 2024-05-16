using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TennisProjekt24.Pages.Bookings
{
    public class AddBookingFancyModel : PageModel
    {
        private IBookingService _bookingService;

        private IMemberService _memberService;

        private ICourtService _courtService;

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
        public Court Court { get; set; }

        [BindProperty]
        public Member CurrentMember {  get; set; }

        //bruges til selectlist

        [BindProperty]
        public List<SelectListItem> MemberList2 { get; set; }

        [BindProperty]
        public int SecondMemberId { get; set; }

        


        public AddBookingFancyModel(IBookingService bookingService, IMemberService memberService, ICourtService courtService)
        {
            _bookingService = bookingService;
            _memberService = memberService;
            _courtService = courtService;
        }

        public IActionResult OnGet(int id, DateOnly date, TimeOnly time)
        {

            try
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
                    Court = _courtService.GetCourt(id);
                    

                    MakeSelectList();


                    return Page();
                }
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;

            }
            return Page();



        }


        public IActionResult OnPost(int id, DateOnly date, TimeOnly time)
        {
            
            int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
            CurrentMember = _memberService.GetMember(sessionMemberId);

            if (SecondMemberId == 0)
            {
                Message = "Du skal vælge en at spille med";

                MakeSelectList();

                return Page();
            }

            if(CheckBookings(CurrentMember.MemberId)==false)
            {
                Message = "Du har for mange bookinger, du må kun booke 4 timer inden for 14 dages periode";

                MakeSelectList();

                return Page();
            }
            if (CheckBookings(SecondMemberId)==false) 
            {
                Message = "Din makker har for mange bookinger";

                MakeSelectList();

                return Page();

            }


            if (CurrentMember.Admin==false) 
            {
                NewBooking.Type = (BookingTypeEnum)1;
            }

           
            NewBooking.Date = date;
            NewBooking.Time = time;
            NewBooking.Court = _courtService.GetCourt(id);
            NewBooking.SecondMemberFull = _memberService.GetMember(SecondMemberId);
            NewBooking.Member = CurrentMember;
            NewBooking.Duration = 1;


            if (_bookingService.CheckAvailability(NewBooking.Court.CourtId, NewBooking.Date, NewBooking.Time) == false)
            {
                Message = "Banen er allerede booket på valgte tidspunkt, vælg et andet tidspunkt og prøv igen";
                return Page();

            }
 

            _bookingService.AddBooking(NewBooking);
            return RedirectToPage("FancyIndex");

        }


        public List<SelectListItem> MakeSelectList()
        {


            if (CurrentMember.Admin)
            {
                List<Member> members = _memberService.GetAllMembers().Where(c => c.MemberId >= 10 && c.MemberId != CurrentMember.MemberId).ToList();
            }
            else
            {

                List<Member> members = _memberService.GetAllMembers().Where(c => c.MemberId >= 10 && c.MemberId != CurrentMember.MemberId).ToList();
            }
            return MemberList2 = members.Select(x => new SelectListItem { Text = x.Name, Value = x.MemberId.ToString() }).ToList();

        }

        public bool CheckBookings(int id)
        {

            DateOnly pastDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly futureDate = pastDate.AddDays(14);

            List<Booking> bookings = _bookingService.GetBookingsByMember(id).Where(c => c.Date >= pastDate && c.Date <= futureDate).ToList();
            if (bookings.Count >= 4 && !_memberService.GetMember(id).Admin)
            {
                return false;
            }


            return true;
            
        }



    }
}
