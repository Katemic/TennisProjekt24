using System.ComponentModel.DataAnnotations;

namespace TennisProjekt24.Models
{
    public enum PracticeTypeEnum
    {
        Barn, Junior, Begynder, Øvet, Alle
    }
    public class Practice
    {
        public int PracticeId {  get; set; }
        [Required(ErrorMessage = "Du skal angive en startdato")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Du skal give træningen en titel")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; } 
        [Required(), Range(1, int.MaxValue, ErrorMessage = "Antal træninger skal være større end 0")]
        public int NoOfTrainings { get; set; }
        [Required(), Range(1,int.MaxValue, ErrorMessage ="Højeste antal deltagere skal være mere end 0")]
        public int MaxNoOfAttendees { get; set; }
        public Instructor? Instructor { get; set; }
        [Required(ErrorMessage ="Vælg type")]
        public PracticeTypeEnum Type { get; set; }
        public List<Member> Members { get; set; }

        public Practice()
        {
            Members = new List<Member>();
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
            Members = new List<Member>();
        }

       


    }
}
