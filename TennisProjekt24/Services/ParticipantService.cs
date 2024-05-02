using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Input;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Services
{
    public class ParticipantService : Connection, IParticipantService
    {
        //public EventService EventService { get; set; }
        //public ParticipantService() { }
        private string addEBsql = "INSERT INTO Participants VALUES(@EventId, @MemberId, @NoOfParticipants, @note)";
        private string deleteSql = "DELETE FROM Participants WHERE EventId=@EventId AND MemberId=@MemberId";
        private string getAllParticipants = "Select * FROM Participants WHERE EventId=@EventId";
        public bool AddEvBooking(Participant participant)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(addEBsql, connection);
                command.Parameters.AddWithValue("@EventId", participant.EventId);
                command.Parameters.AddWithValue("@MemberId", participant.MemberId);
                command.Parameters.AddWithValue("@NoOfParticipants", participant.NoOfParticipants);
                command.Parameters.AddWithValue("@note", participant.Note);
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

        public List<Event> GetAllEventsByParticipant(int memberId)
        {
            throw new NotImplementedException();
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
                        int memberId = reader.GetInt32("MemberId");
                        int noOfParticipants = reader.GetInt32("NoOfParticipants");
                        string description = reader.GetString("note");
                        Participant participant = new Participant(evId, memberId, noOfParticipants, description);
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
    }
}

