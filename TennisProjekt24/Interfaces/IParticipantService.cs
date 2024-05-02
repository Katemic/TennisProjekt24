using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IParticipantService
    {
        bool AddEvBooking(Participant participant);
        bool DeleteEvBooking(int memberId, int eventId);
        List<Member> GetAllParticipants(int eventId);
        List<Event> GetAllEvMember(int memberId);
    }
}
