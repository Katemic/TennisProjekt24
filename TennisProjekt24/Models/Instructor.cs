namespace TennisProjekt24.Models
{
    public class Instructor
    {

        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

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
