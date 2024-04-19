using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IPracticeService
    {
        Practice GetPractice(int id);
        bool AddPractice(Practice practice);
        bool DeletePractice(int id);
        bool UpdatePractice(Practice practice, int id);
        List<Practice> GetAllPractices();
        List<Practice> GetPracticeByType();


    }
}
