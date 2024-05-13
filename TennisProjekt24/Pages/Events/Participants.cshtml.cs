using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Events
{
    public class ParticipantsModel : PageModel
    {
        private IParticipantService _participantService;
        private IMemberService _memberService;
        public List<Participant> Participants { get; set; }
        public int CurrentAttendees { get; set; }
        public Member CurrentMember { get; set; }
        public ParticipantsModel(IParticipantService participantService, IMemberService memberService)
        {
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
                Participants = _participantService.GetAllParticipants(eventId);
                foreach (var participant in Participants)
                {
                    CurrentAttendees = CurrentAttendees + participant.NoOfParticipants;
                }
                return Page();
            }
            
        }
    }
}
