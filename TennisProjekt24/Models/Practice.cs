namespace TennisProjekt24.Models
{
    public class Practice
    {

        public DateTime StartDate { get; set; }
        public string Title { get; set; }
        public int NoOfTrainings { get; set; }
        public int MaxNoOfAteendees { get; set; }
        public int InstructorId { get; set; }
        public PracticeTypeEnum Type { get; set; }

        public Practice()
        {
                
        }

        public enum PracticeTypeEnum
        {
            Kids, Junior, Beginner, Intermediate, Advanced, ForEveryone
        }


    }
}
