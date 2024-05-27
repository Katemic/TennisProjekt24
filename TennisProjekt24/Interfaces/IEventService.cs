using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IEventService
    {
        bool AddEvent(Event ev);

        bool DeleteEvent(int eventId);

        bool UpdateEvent(int eventId, Event ev);

        Event GetEvent(int eventId);

        List<Event> GetAllEvents();
    }
}
