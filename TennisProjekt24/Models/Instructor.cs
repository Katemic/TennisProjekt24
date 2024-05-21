using System.ComponentModel.DataAnnotations;

namespace TennisProjekt24.Models
{
    public class Instructor
    {

        public int InstructorId { get; set; }
        [Required(ErrorMessage ="Name must be assigned")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone number must be assigned"), MaxLength(11)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Description cannot be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "For the sake of the participants, a picture is required of the instructor")]
        public string? Image { get; set; }

        public Instructor()
        {
            
        }
        public Instructor(int id, string name, string phonNo, string desc, string img)
        {
            InstructorId = id;
            Name = name;
            PhoneNumber = phonNo;
            Description = desc;
            Image = img;
        }
    }
}
