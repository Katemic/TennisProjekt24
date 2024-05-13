using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Input;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Pages.Members;
using TennisProjekt24.Services;

namespace TennisProjekt24.Services
{
    public class ParticipantService : Connection, IParticipantService
    {
        //public EventService EventService { get; set; }
        private IMemberService _memberService;
        private IEventService _eventService;
        public ParticipantService(IMemberService memberService, IEventService eventService)
        {
            _memberService = memberService;
            _eventService = eventService;
        }

        private string addEBsql = "INSERT INTO Participants VALUES(@EventId, @MemberId, @NoOfParticipants, @note)";
        private string deleteSql = "DELETE FROM Participants WHERE EventId=@EventId AND MemberId=@MemberId";
        private string getAllParticipants = "Select * FROM Participants WHERE EventId=@EventId";
        private string getAllEventsByParticipant = "Select * FROM Participants WHERE MemberId=@MemberId";
        private string getParticipant = "SELECT EventId, MemberId, NoOfParticipants, note FROM Participants WHERE EventId=@EventId AND MemberId=@MemberId";
        private string updateSql = "Update Participants SET NoOfParticipants = @NoOfParticipants, note = @note WHERE EventId=@EventId AND MemberId=@MemberId";
        public bool AddEvBooking(Participant participant)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(addEBsql, connection);
                command.Parameters.AddWithValue("@EventId", participant.Event.EventId);
                command.Parameters.AddWithValue("@MemberId", participant.Member.MemberId);
                command.Parameters.AddWithValue("@NoOfParticipants", participant.NoOfParticipants);
                if (participant.Note != null)
                {
                    command.Parameters.AddWithValue("@note", participant.Note);
                }
                else
                {
                    command.Parameters.AddWithValue("@note", DBNull.Value);
                }
                try
                {
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();

                    if (noOfRows == 1)
                    {
                        return true;
                    }

                    return false;
                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine("Database error");

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);

                }
                return false;
            }
        }

        public bool DeleteEvBooking(int memberId, int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return true;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error " + ex.Message);
                }
            }
            return false;
        }

        public List<Participant> GetAllEventsByParticipant(int memberId)
        {
            List<Participant> participants = new List<Participant>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(getAllEventsByParticipant, connection);
                    //command.Parameters.AddWithValue("@EventId", participant.Event.EventId);
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int evId = reader.GetInt32("EventId");
                        Event ev = _eventService.GetEvent(evId);
                        int memberID = reader.GetInt32("MemberId");
                        int noOfParticipants = reader.GetInt32("NoOfParticipants");
                        string? description = null;
                        if (!reader.IsDBNull(3))
                        {
                            description = reader.GetString(3);
                        }
                        Member member = _memberService.GetMember(memberID);
                        Participant p = new Participant(ev, member, noOfParticipants, description);
                        participants.Add(p);
                    }
                    reader.Close();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error " + ex.Message);
                }
            }
            return participants;
        }

        public List<Participant> GetAllParticipants(int eventId)
        {
            List<Participant> participants = new List<Participant>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(getAllParticipants, connection);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int evId = reader.GetInt32("EventId");
                        Event ev = _eventService.GetEvent(evId);
                        int memberId = reader.GetInt32("MemberId");
                        int noOfParticipants = reader.GetInt32("NoOfParticipants");
                        string? description = null;
                        if (!reader.IsDBNull(3))
                        {
                            description = reader.GetString(3);
                        }
                        Member member = _memberService.GetMember(memberId);
                        Participant participant = new Participant(ev, member, noOfParticipants, description);
                        participants.Add(participant);
                    }
                    reader.Close();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("der var en database error: " + sqlEx.Message);

                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);

                    throw ex;
                }
                finally
                {

                }
            }

            return participants;
        }

        public Participant GetParticipant(int eventId, int memberId)
        {
            Participant p = new Participant();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(getParticipant, connection);
                    command.Parameters.AddWithValue("@EventId", eventId);
                    command.Parameters.AddWithValue("@MemberId", memberId);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int evId = reader.GetInt32("EventId");
                        int mId = reader.GetInt32("MemberId");
                        int noOfParticipants = reader.GetInt32("NoOfParticipants");
                        string note = !reader.IsDBNull(reader.GetOrdinal("note")) ? reader.GetString(reader.GetOrdinal("note")) : null;

                        Member member = _memberService.GetMember(memberId);
                        Event ev = _eventService.GetEvent(eventId);
                        p = new Participant(ev, member, noOfParticipants, note);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return p;
        }


        public bool UpdateParticipant(Participant p)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@NoOfParticipants", p.NoOfParticipants);
                    SqlParameter noteParameter = new SqlParameter("@note", SqlDbType.VarChar);
                    noteParameter.Value = (object)p.Note ?? DBNull.Value;
                    command.Parameters.Add(noteParameter);
                    command.Parameters.AddWithValue("@EventId", p.Event.EventId);
                    command.Parameters.AddWithValue("@MemberId", p.Member.MemberId);

                    command.Connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result == 1;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return false;
            }
        }
    }
}

