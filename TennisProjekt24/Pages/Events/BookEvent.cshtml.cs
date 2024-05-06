using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Events
{
    public class BookEventModel : PageModel
    {
        private IEventService _eventService;
        private IParticipantService _participantService;
        private IMemberService _memberService;
        [BindProperty]
        public Event EventBook { get; set; }
        [BindProperty]
        public Member CurrentMember { get; set; }
        //public List<Member> members = new List<Member>();
        [BindProperty]
        public Participant Attendee { get; set; }
        //[BindProperty]
        //public int NoOfParticipants { get; set; }
        //[BindProperty]
        //public string note { get; set; }

        public BookEventModel(IEventService eventService, IParticipantService participantService, IMemberService memberService)
        {
            Attendee = new Participant();
            _eventService = eventService;
            _participantService = participantService;
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
                EventBook = _eventService.GetEvent(eventId);
                return Page();
            }
            //members = _memberService.GetAllMembers();
        }
        public IActionResult OnPost(int eventId)
        {
            int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
            CurrentMember = _memberService.GetMember(sessionMemberId);
            Attendee.Event = _eventService.GetEvent(eventId);
            Attendee.Member = CurrentMember;
            //if (!ModelState.IsValid)
            //{
            //    EventBook = _eventService.GetEvent(eventId);
            //    members = _memberService.GetAllMembers();
            //    return Page();
            //}
            
            //Attendee.NoOfParticipants = NoOfParticipants;
            //Attendee.Note = note;



            _participantService.AddEvBooking(Attendee);
            return RedirectToPage("Index");
        }
    }
}
