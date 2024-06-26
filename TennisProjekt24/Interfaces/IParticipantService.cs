﻿using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IParticipantService
    {
        bool AddEvBooking(Participant participant);
        bool DeleteEvBooking(int memberId, int eventId);
        List<Participant> GetAllParticipants(int eventId);
        List<Participant> GetAllEventsByParticipant(int memberId);
        Participant GetParticipant(int eventId, int memberId);
        bool UpdateParticipant(Participant p);
    }
}
