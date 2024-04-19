using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface ICourtService
    {
        bool AddCourt(Court court);
        bool DeleteCourt(int CourtId);
        bool UpdateCourt(int CourtId, Court court);
        Court GetCourt(int CourtId);
        List<Court> GetAllCourts();
        void ChangeAvailability(int CourtId);
    }
}
