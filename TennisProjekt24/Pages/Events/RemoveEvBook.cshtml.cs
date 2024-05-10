using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Pages.Events
{
    public class RemoveEvBookModel : PageModel
    {

        [BindProperty]
        public Event EventBook { get; set; }
        public Member CurrentMember { get; set; }
        List <Participant> Participants { get; set; }

        private IParticipantService _participantService;
        private IEventService _eventService;
        private IMemberService _memberService;
        public RemoveEvBookModel(IParticipantService participantService, IEventService eventService, IMemberService memberService)
        {
            _participantService = participantService;
            _eventService = eventService;
            _memberService = memberService;
        }
        public IActionResult OnGet(int eventId)
        {
            if (HttpContext.Session.GetInt32("MemberId") == null)
            {
                return RedirectToPage("/Members/LogIn");
            }
            else
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
                Participants = _participantService.GetAllParticipants(eventId);
                EventBook = _eventService.GetEvent(eventId);
                return Page();
            }
        }
        public IActionResult OnPost(int memberId, int eventId)
        {
            _participantService.DeleteEvBooking(memberId, eventId);
            return RedirectToPage("Index");
        }
    }
}