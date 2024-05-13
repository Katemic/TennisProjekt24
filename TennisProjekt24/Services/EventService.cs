using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services
{
    public class EventService : Connection, IEventService
    {
        private IMemberService _memberService;
        public EventService(IMemberService memberService)
        {
            _memberService = memberService;
        }
        private string insertSql = "INSERT INTO Events VALUES(@DATE, @TITLE, @DESCRIPTION, @PLACE, @MEMBERID)";
        private string getSql = "SELECT EventId, Date, Title, Description, Place, MemberId FROM Events WHERE EventId=@ID";
        private string updateSql = "UPDATE Events SET Date = @Date, Title = @Title, Description = @Description, Place = @Place, MemberId = @MemberId WHERE EventId=@ID";
        private string getallSql = "SELECT * FROM Events";
        private string deleteSql = "DELETE FROM Events WHERE EventId=@ID";
        public bool AddEvent(Event ev)
        {            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    //command.Parameters.AddWithValue("@EventId", ev.EventId);
                    command.Parameters.AddWithValue("@Date", ev.Date);
                    command.Parameters.AddWithValue("@Title", ev.Title);
                    command.Parameters.AddWithValue("@DESCRIPTION", ev.Description);
                    command.Parameters.AddWithValue("@PLACE", ev.Place);
                    command.Parameters.AddWithValue("@MEMBERID", ev.Member.MemberId);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
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
            return false;
        }

        public bool DeleteEvent(int eventId)
        {
            Event ev = GetEvent(eventId);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand commandDel = new SqlCommand(deleteSql, connection);
                    commandDel.Parameters.AddWithValue("@ID", eventId);

                    commandDel.Connection.Open();
                    int result = commandDel.ExecuteNonQuery();

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
            return false;
        }

        public List<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(getallSql, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int eventId = reader.GetInt32("EventId");
                        DateTime date = reader.GetDateTime("Date");
                        string title = reader.GetString("Title");
                        string description = reader.GetString("Description");
                        string place = reader.GetString("Place");
                        int memberId = reader.GetInt32("MemberId");
                        Member member = _memberService.GetMember(memberId);
                        Event ev = new Event(eventId, date, title, description, place, member);
                        events.Add(ev);
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
            return events;
        }

        public Event GetEvent(int eventId)
        {
            Event ev = new Event();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(getSql, connection);
                    command.Parameters.AddWithValue("@ID", eventId);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int evId = reader.GetInt32("EventId");
                        DateTime dateTime = reader.GetDateTime("Date");
                        string title = reader.GetString("title");
                        string description = reader.GetString("Description");
                        string place = reader.GetString("Place");
                        int memberId = reader.GetInt32("MemberId");
                        Member member = _memberService.GetMember(memberId);
                        ev = new Event(evId, dateTime, title, description, place, member);
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
            return ev;
        }

        public bool UpdateEvent(Event ev)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@ID", ev.EventId);
                    command.Parameters.AddWithValue("@Date", ev.Date);
                    command.Parameters.AddWithValue("@Title", ev.Title);
                    command.Parameters.AddWithValue("@DESCRIPTION", ev.Description);
                    command.Parameters.AddWithValue("@PLACE", ev.Place);
                    command.Parameters.AddWithValue("@MEMBERID", ev.Member.MemberId);

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
