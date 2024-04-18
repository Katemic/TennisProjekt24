namespace TennisProjekt24.Models
{
    public class Booking
    {
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int MemberId { get; set; }
        public int SecondMember { get; set; }
        public int Court { get; set; }
        public BookingTypeEnum Type { get; set; }
        public string Note { get; set; }


        public Booking()
        {
            
        }


        public enum BookingTypeEnum
        {
            Member, Event, Practice
        }

    }
}
