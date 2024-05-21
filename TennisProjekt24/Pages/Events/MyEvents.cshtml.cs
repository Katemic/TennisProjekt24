using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Events
{
    public class MyEventsModel : PageModel
    {
        private IParticipantService _participantService;
        private IEventService _eventService;
        private IMemberService _memberService;
        public Member CurrentMember { get; set; }
        public List<Participant> Participants { get; set; }
        public List<Participant> ParticipantsFuture { get; set; }
        public MyEventsModel(IParticipantService participantService, IEventService eventService, IMemberService memberService)
        {
            _participantService = participantService;
            _eventService = eventService;
            _memberService = memberService;
            ParticipantsFuture = new List<Participant>();
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
                //memberId = sessionMemberId;
                Participants = _participantService.GetAllEventsByParticipant(sessionMemberId);
                foreach (var participant in Participants)
                {
                    if (participant.Event.Date > DateTime.Now)
                    {
                        ParticipantsFuture.Add(participant);
                    }
                }
                //EventBook = _eventService.GetEvent(eventId);
                return Page();
            }
        }
    }
}
