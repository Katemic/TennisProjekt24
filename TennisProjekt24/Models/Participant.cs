using System.ComponentModel.DataAnnotations;

namespace TennisProjekt24.Models
{
    public class Participant
    {

        public int EventId { get; set; }
        public Member Member { get; set; }
        [Required(ErrorMessage = "Vælg antal deltagere")]
        [Range(1, 5, ErrorMessage = "Antal skal være mellem 1 og 5")]
        public int NoOfParticipants { get; set; }
        public string Note { get; set; }

        public Participant()
        {
            
        }
        public Participant(int eventId, Member member, int noOfParticipants, string note)
        {
            EventId = eventId;
            Member = member;
            NoOfParticipants = noOfParticipants;
            Note = note;
        }

    }
}
