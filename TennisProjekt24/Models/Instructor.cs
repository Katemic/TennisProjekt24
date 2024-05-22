using System.ComponentModel.DataAnnotations;

namespace TennisProjekt24.Models
{
    public class Instructor
    {

        public int InstructorId { get; set; }
        [Required(ErrorMessage ="Du skal anvive et navn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Der skal være et telefonnummer"), MaxLength(11)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Der skal være en beskrivelse")]
        public string Description { get; set; }
        [Required(ErrorMessage = "For medlemmernes skyld skal der være billede på trænerne")]
        public string? Image { get; set; }

        public Instructor()
        {
            
        }
        public Instructor(int id, string name, string phonNo, string desc, string? img)
        {
            InstructorId = id;
            Name = name;
            PhoneNumber = phonNo;
            Description = desc;
            Image = img;
        }
    }
}
