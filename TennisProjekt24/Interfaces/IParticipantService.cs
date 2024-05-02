using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IParticipantService
    {
        bool AddEvBooking(Participant participant);
        bool DeleteEvBooking(int memberId, int eventId);
        List<Participant> GetAllParticipants(int eventId);
        List<Event> GetAllEventsByParticipant(int memberId);
    }
}
