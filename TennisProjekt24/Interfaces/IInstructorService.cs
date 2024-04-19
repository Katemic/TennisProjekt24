using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IInstructorService
    {

        bool AddInstructor(Instructor instructor);
        bool DeleteInstructor(int instructorId);
        bool UpdateInstructor(Instructor instructor, int instructorId);
        Instructor GetInstructor(int instructorId);
        List<Instructor> GetAllInstructors();
    }
}
