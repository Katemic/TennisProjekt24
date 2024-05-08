using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {

        private IMemberService _memberService;
        private IBookingService _bookingService;

        List<Booking> bookings {  get; set; }

        public Member MemberToDelete { get; set; }

        public DeleteMemberModel(IMemberService memberService, IBookingService bookingService)
        {
            _memberService = memberService;
            _bookingService = bookingService;
        }


        public void OnGet(int id)
        {
            MemberToDelete = _memberService.GetMember(id);
        }

        public IActionResult OnPost(int id) 
        {
            if ((int)HttpContext.Session.GetInt32("MemberId") == id)
            {
                HttpContext.Session.Remove("MemberId");
            }
            
            
            bookings = _bookingService.GetBookingsByMember(id).Where(c=>c.SecondMemberFull.MemberId == id).ToList();

            if (bookings.Count != 0 ) 
            {
                foreach(var item in bookings)
                {
                    item.SecondMemberFull = _memberService.GetMember(1);
                    _bookingService.updateBooking(item, item.BookingId);
                }

                _memberService.DeleteMember(id);
                return RedirectToPage("Index");

            }
            else
            {
                _memberService.DeleteMember(id);
                return RedirectToPage("Index");
            }


        }


    }
}
