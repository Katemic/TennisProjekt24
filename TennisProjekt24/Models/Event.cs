namespace TennisProjekt24.Models
{
    public class Event
    {

        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public EventTypeEnum Type { get; set; }
        public int MemberId { get; set; }


        public enum EventTypeEnum
        {
            Social, Workday, Intro
        }

        public Event()
        {
            
        }

    }
}
