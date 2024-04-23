using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using static TennisProjekt24.Models.Practice;

namespace TennisProjekt24.Services
{
    public class PracticeService : Connection, IPracticeService
    {
        private string _getAllPracticesString = $"SELECT PracticeId, Date, Title, NoOfTrainings, MaxNoOfAteendees, InstructorId, Type FROM Practices";
        private string _getPracticeString = $"SELECT * FROM Practices WHERE PracticeID = @ID";
        private string _addPracticeString = $"INSERT INTO Practices VALUES(@Date, @Title, @NoTrain, @MaxAtendees,  @Type , @InstructorId)";
        private string _deletePracticeString = $"DELETE FROM Practices WHERE PracticeId = @ID";
        private string _updatePracticeString = $"UPDATE Practices SET Date = @Date, Title = @Title, NoOfTrainings = @NoTrain, " +
                                                "MaxNoOfAteendees = @MaxAtendees, Type = @Type , InstructorId = @InstructorId WHERE PracticeId = @ID";

        public bool AddPractice(Practice practice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addPracticeString, connection);
                    //command.Parameters.AddWithValue("@ID", practice.PracticeId);
                    command.Parameters.AddWithValue("@Date", practice.StartDate);
                    command.Parameters.AddWithValue("@Title", practice.Title);
                    command.Parameters.AddWithValue("@NoTrain", practice.NoOfTrainings);
                    command.Parameters.AddWithValue("@MaxAtendees", practice.MaxNoOfAttendees);
                    command.Parameters.AddWithValue("@InstructorId", practice.InstructorId);
                    command.Parameters.AddWithValue("@Type", practice.Type);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
            }
            return false;
        }

        public bool DeletePractice(int id)
        {
            Practice practice = GetPractice(id);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(_deletePracticeString, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
            }
            return false;
        }

        public List<Practice> GetAllPractices()
        {
            List<Practice> practices = new List<Practice>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(_getAllPracticesString, connection);
                    commmand.Connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        int practiceId = reader.GetInt32("PracticeID");//.GetInt32(0);
                        DateTime date = reader.GetDateTime("Date");
                        string title = (string)reader["Title"];
                        int NoOfTrainings = reader.GetInt32("NoOfTrainings");
                        int MaxNoOfAteendees = reader.GetInt32("MaxNoOfAteendees");
                        int InstructorId = reader.GetInt32("InstructorId");
                        Enum.TryParse((string)reader["Type"], out PracticeTypeEnum Type);
                        Practice practice = new Practice(practiceId, date, title, NoOfTrainings, MaxNoOfAteendees, InstructorId, Type);
                        practices.Add(practice);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
                finally
                {
                 
                }
            }
            return practices;
        }

        public Practice GetPractice(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(_getPracticeString, connection);
                    commmand.Parameters.AddWithValue("@ID", id);
                    commmand.Connection.Open();

                    SqlDataReader reader = commmand.ExecuteReader();
                    if (reader.Read())
                    {
                        int practiceId = reader.GetInt32("PracticeID");//.GetInt32(0);
                        DateTime date = reader.GetDateTime("Date");
                        string title = (string)reader["Title"];
                        int NoOfTrainings = reader.GetInt32("NoOfTrainings");
                        int MaxNoOfAteendees = reader.GetInt32("MaxNoOfAteendees");
                        int InstructorId = reader.GetInt32("InstructorId");
                        Enum.TryParse((string)reader["Type"], out PracticeTypeEnum Type);
                        Practice practice = new Practice(practiceId, date, title, NoOfTrainings, MaxNoOfAteendees, InstructorId, Type);
                        return practice;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
                finally
                {
                    //her kommer man altid
                }
            }
            return null;
        }

        public List<Practice> GetPracticeByType()
        {
            throw new NotImplementedException();
        }

        public bool UpdatePractice(Practice practice, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(_updatePracticeString, connection))
                {
                    command.Parameters.AddWithValue("@Date", practice.StartDate);
                    command.Parameters.AddWithValue("@Title", practice.Title);
                    command.Parameters.AddWithValue("@NoTrain", practice.NoOfTrainings);
                    command.Parameters.AddWithValue("@MaxAtendees", practice.MaxNoOfAttendees);
                    command.Parameters.AddWithValue("@InstructorId", practice.InstructorId);
                    command.Parameters.AddWithValue("@Type", practice.Type);
                    command.Parameters.AddWithValue("@ID", id);
                    command.Connection.Open();

                    int noOfRows = command.ExecuteNonQuery();
                    if (noOfRows == 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
    }
}
