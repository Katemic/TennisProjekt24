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
        [Required, MaxLength(500)]
        public string Description { get; set; } 
        [Required(), Range(1, int.MaxValue, ErrorMessage = "Number of trainings must be more than 0")]
        public int NoOfTrainings { get; set; }
        [Required(), Range(1,int.MaxValue, ErrorMessage ="Max attendees must be more than 0")]
        public int MaxNoOfAttendees { get; set; }
        [Required(ErrorMessage ="ID must not be empty")]
        public Instructor Instructor { get; set; }
        [Required(ErrorMessage ="Choose a practice type")]
        public PracticeTypeEnum Type { get; set; }

        public Practice()
        {
                
        }
        public Practice(int practiceId, DateTime startDate, string title, string desc, int noOfTrainings, int maxNoOfAteendees, Instructor instructor, PracticeTypeEnum type)
        {
            PracticeId = practiceId;
            StartDate = startDate;
            Title = title;
            Description = desc;
            NoOfTrainings = noOfTrainings;
            MaxNoOfAttendees = maxNoOfAteendees;
            Instructor = instructor;
            Type = type;
        }

       


    }
}
