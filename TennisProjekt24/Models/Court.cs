namespace TennisProjekt24.Models
{
    public class Court
    {

        public int CourtId { get; set; }
        public bool Outdoor { get; set; }
        public int CourtNumber { get; set; }
        public CourtTypeEnum CourtType { get; set; }
        public bool Availability { get; set; }


        public Court()
        {
            
        }


        public enum CourtTypeEnum
        {
            Padel, Tennis, PickleBall
        }

    }
}
