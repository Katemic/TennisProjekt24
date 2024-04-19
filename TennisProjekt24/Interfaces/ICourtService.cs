using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface ICourtService
    {
        bool AddCourt();
        bool DeleteCourt();
        bool UpdateCourt();
        Court GetCourt();
        List<Court> GetAllCourts();
        void ChangeAvailability();
    }
}
