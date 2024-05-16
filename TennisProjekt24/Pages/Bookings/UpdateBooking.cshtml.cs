using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client.Extensibility;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Bookings
{
    public class UpdateBookingModel : PageModel
    {

        private IBookingService _bookingService;
        private IMemberService _memberService;

        [BindProperty]
        public Booking BookingToUpdate {  get; set; }

        public Member CurrentMember { get; set; }

        [BindProperty]
        public List<SelectListItem> MemberList2 { get; set; }

        [BindProperty]
        public int SecondMemberId { get; set; }

        public UpdateBookingModel(IBookingService bookingService, IMemberService memberService)
        {
            _bookingService = bookingService;
            _memberService = memberService;
            
        }


        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("LogIn");
            }
            else
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
                BookingToUpdate = _bookingService.GetBooking(id);

                MakeSelectList();
                
                
                return Page();
            }
        }


        public IActionResult OnPost()
        {

            
            
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            BookingToUpdate.SecondMemberFull = _memberService.GetMember(SecondMemberId);
            _bookingService.updateBooking(BookingToUpdate,BookingToUpdate.BookingId);
            return RedirectToPage("FancyIndex");

        }


        public List<SelectListItem> MakeSelectList()
        {
            List<Member> members = _memberService.GetAllMembers().Where(c => c.MemberId >= 10
                && c.MemberId != BookingToUpdate.Member.MemberId && c.MemberId != BookingToUpdate.SecondMemberFull.MemberId).ToList(); ;

            return MemberList2 = members.Select(x => new SelectListItem { Text = x.Name, Value = x.MemberId.ToString() }).ToList();
        }


    }
}
