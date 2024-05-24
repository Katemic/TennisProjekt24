using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {

        private IMemberService _memberService;
        private IBookingService _bookingService;
        private IBuddyForumService _buddyForumService;

        List<Booking> bookings {  get; set; }
        List<BuddyForum> buddyForums { get; set; }

        public Member MemberToDelete { get; set; }

        public DeleteMemberModel(IMemberService memberService, IBookingService bookingService, IBuddyForumService buddyForumService)
        {
            _memberService = memberService;
            _bookingService = bookingService;
            _buddyForumService = buddyForumService;
        }


        public void OnGet(int id)
        {
            try
            {
                MemberToDelete = _memberService.GetMember(id);
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

                if ((int)HttpContext.Session.GetInt32("MemberId") == id)
                {
                    HttpContext.Session.Remove("MemberId");
                }

                buddyForums = _buddyForumService.GetAllPosts().Where(p => p.Poster.MemberId == id).ToList();
                if (buddyForums.Count > 0)
                    foreach (var buddyForum in buddyForums)
                        _buddyForumService.DeletePost(buddyForum.PostId);

                bookings = _bookingService.GetBookingsByMember(id).Where(c => c.SecondMemberFull.MemberId == id).ToList();

                if (bookings.Count != 0)
                {
                    foreach (var item in bookings)
                    {
                        item.SecondMemberFull = _memberService.GetMember(1);
                        _bookingService.updateBooking(item, item.BookingId);
                    }
                }

                _memberService.DeleteMember(id);
                return RedirectToPage("Index");

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
