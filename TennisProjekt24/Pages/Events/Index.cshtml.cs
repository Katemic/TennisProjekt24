using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Pages.Events
{
    public class IndexModel : PageModel
    {
        private IEventService _eventService;
        private IMemberService _memberService;
        private IParticipantService _participantService;

        public List<Event> Events { get; set; }
        public List<Event> EventsFuture{ get; set; }
        public Member CurrentMember { get; set; }
        public List<Participant> Participants { get; set; }

        public IndexModel(IEventService eventService, IMemberService memberService, IParticipantService participantService)
        {
            _eventService = eventService;
            _memberService = memberService;
            _participantService = participantService;
            EventsFuture = new List<Event>();
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
                Events = _eventService.GetAllEvents();
                foreach (Event e in Events)
                {
                    if (e.Date >  DateTime.Now)
                    {
                        EventsFuture.Add(e);
                    }
                }
                Participants = _participantService.GetAllEventsByParticipant(sessionMemberId);
                return Page();
            }

        }
    }
}
