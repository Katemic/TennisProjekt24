using Microsoft.Data.SqlClient;
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

        public List<Event> GetAllEvMember(int memberId)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetAllParticipants(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
