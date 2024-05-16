using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

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
                    //List<Member> members = _memberService.GetAllMembers().Where(c => c.MemberId >=10 && c.MemberId != CurrentMember.MemberId).ToList();

                    //MemberList2 = members.Select(x => new SelectListItem { Text = x.Name, Value = x.MemberId.ToString() }).ToList();

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

            DateOnly pastDate = date.AddDays(-1);
            DateOnly futureDate = date.AddDays(14);
            
            int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
            CurrentMember = _memberService.GetMember(sessionMemberId);

            if (SecondMemberId == 0)
            {
                Message = "Du skal v�lge en at spille med";

                MakeSelectList();

                return Page();
            }


            List<Booking> bookings = _bookingService.GetBookingsByMember(CurrentMember.MemberId).Where(c => c.Date >= pastDate && c.Date < futureDate).ToList();
            if (bookings.Count >=4 && CurrentMember.Admin==false)
            {
                Message = "Du har for mange bookinger, du m� kun booke 4 timer inden for 14 dages periode";

                //CourtId= id;
                //Time = time;
                //Date = date;

                MakeSelectList();

                return Page();
            }
            NewBooking.SecondMemberFull = _memberService.GetMember(SecondMemberId);
            bookings = _bookingService.GetBookingsByMember(SecondMemberId).Where(c => c.Date > pastDate && c.Date < futureDate).ToList();
            if (bookings.Count >= 4 &&  NewBooking.SecondMemberFull.Admin == false)
            {
                Message = "Din makker har for mange bookinger";

                //CourtId= id;
                //Time = time;
                //Date = date;

                MakeSelectList();

                return Page();
            }

            if(CurrentMember.Admin==false) 
            {
                NewBooking.Type = (BookingTypeEnum)1;
            }
            
            NewBooking.Date = date;
            NewBooking.Time = time;
            NewBooking.Court = _courtService.GetCourt(id);
            //NewBooking.SecondMemberFull = _memberService.GetMember(SecondMemberId);
            NewBooking.Member = CurrentMember;
            NewBooking.Duration = 1;

            
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}



            if (_bookingService.CheckAvailability(NewBooking.Court.CourtId, NewBooking.Date, NewBooking.Time) == false)
            {
                Message = "Banen er allerede booket p� valgte tidspunkt, v�lg et andet tidspunkt og pr�v igen";
                return Page();

            }

            

            _bookingService.AddBooking(NewBooking);
            return RedirectToPage("FancyIndex");

        }


        public List<SelectListItem> MakeSelectList()
        {

            List<Member> members = _memberService.GetAllMembers().Where(c => c.MemberId >= 10 && c.MemberId != CurrentMember.MemberId).ToList(); ;

            return MemberList2 = members.Select(x => new SelectListItem { Text = x.Name, Value = x.MemberId.ToString() }).ToList();

        }

        public bool CheckBookings(int id)
        {


            return false;
            
        }



    }
}
