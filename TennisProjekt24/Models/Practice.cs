using System.ComponentModel.DataAnnotations;

namespace TennisProjekt24.Models
{
    public enum PracticeTypeEnum
    {
        Kids, Junior, Beginner, Intermediate, Advanced, ForEveryone
    }
    public class Practice
    {
        public int PracticeId {  get; set; }
        [Required(ErrorMessage = "Date must not be empty")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Title must not be empty")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Number of training must not be 0")]
        public int NoOfTrainings { get; set; }
        [Required(ErrorMessage = "Max attendees must not be 0")]
        public int MaxNoOfAttendees { get; set; }
        [Required(ErrorMessage ="ID must not be empty")]
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

       


    }
}
