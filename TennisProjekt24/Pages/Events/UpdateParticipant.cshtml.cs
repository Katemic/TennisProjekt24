using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Pages.Events
{
    public class UpdateParticipantModel : PageModel
    {
        private IParticipantService _participantService;
        [BindProperty]
        public Participant ParticipantToUpdate { get; set; }
        public string Message { get; set; }

        public UpdateParticipantModel(IParticipantService participantService)
        {
            _participantService = participantService;
        }
        public void OnGet(int eventId, int memberId)
        {
            ParticipantToUpdate = _participantService.GetParticipant(eventId, memberId);
        }
        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (ParticipantToUpdate.NoOfParticipants > 5 || ParticipantToUpdate.NoOfParticipants < 1)
            {
                Message = "Vælg antal mellem 1 og 5";
                //EventBook = _eventService.GetEvent(eventId);
                return Page();
            }
            _participantService.UpdateParticipant(ParticipantToUpdate);
            return RedirectToPage("Index");
        }
    }
}
