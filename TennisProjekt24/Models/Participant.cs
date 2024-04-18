namespace TennisProjekt24.Models
{
    public class Participant
    {

        public int EventId { get; set; }
        public int MemberId { get; set; }
        public int NoOfParticipants { get; set; }
        public string Note { get; set; }

        public Participant()
        {
            
        }


    }
}
