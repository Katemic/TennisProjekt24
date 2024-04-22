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
        private string _getAllPracticesString = $"SELECT PracticeId, Date, Title, NoOfTrainings, MaxNoOfAteendees, Type, InstructorId FROM Practices";
        private string _getPracticeString = $"SELECT * FROM Practices WHERE PracticeID = @ID";
        private string _AddPracticeString = $"INSERT INTO Practices VALUES(@ID, @Date, @Title, @NoTrain, @MaxAtendees, @InstructorId, @Type)";

        public bool AddPractice(Practice practice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_AddPracticeString, connection);
                    command.Parameters.AddWithValue("@ID", practice.PracticeId);
                    command.Parameters.AddWithValue("@Date", practice.StartDate);
                    command.Parameters.AddWithValue("@Title", practice.Title);
                    command.Parameters.AddWithValue("", practice.NoOfTrainings);
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
