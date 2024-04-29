namespace TennisProjekt24.Models
{
    public class Practice
    {
        public int PracticeId {  get; set; }
        public DateTime StartDate { get; set; }
        public string Title { get; set; }
        public int NoOfTrainings { get; set; }
        public int MaxNoOfAttendees { get; set; }
        public int InstructorId { get; set; }
        public PracticeTypeEnum Type { get; set; }

        public Practice()
        {
                
        }
        public Practice(int practiceId, DateTime startDate, string title, int noOfTrainings, int maxNoOfAteendees, int instructorId, PracticeTypeEnum type)
        {
            PracticeId = practiceId;
            StartDate = startDate;
            Title = title;
            NoOfTrainings = noOfTrainings;
            MaxNoOfAttendees = maxNoOfAteendees;
            InstructorId = instructorId;
            Type = type;
        }

        public enum PracticeTypeEnum
        {
            Kids, Junior, Beginner, Intermediate, Advanced, ForEveryone
        }


    }
}
