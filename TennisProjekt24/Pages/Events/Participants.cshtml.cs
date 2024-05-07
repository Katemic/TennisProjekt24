using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.Events
{
    public class ParticipantsModel : PageModel
    {
        private IParticipantService _participantService;
        public List<Participant> Participants { get; set; }
        public int CurrentAttendees { get; set; }
        public ParticipantsModel(IParticipantService participantService)
        {
            _participantService = participantService;
            
        }
        public void OnGet(int eventId)
        {
            Participants = _participantService.GetAllParticipants(eventId);
            foreach (var participant in Participants)
            {
                CurrentAttendees = CurrentAttendees +participant.NoOfParticipants;
            }
        }
    }
}
