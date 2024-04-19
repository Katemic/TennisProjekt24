using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface ICourtService
    {
        bool AddCourt(Court court);
        bool DeleteCourt(int courtId);
        bool UpdateCourt(int courtId, Court court);
        Court GetCourt(int courtId);
        List<Court> GetAllCourts();
        void ChangeAvailability(int courtId);
    }
}
