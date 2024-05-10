using Microsoft.AspNetCore.Http;
using System.Data;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using static TennisProjekt24.Models.Practice;
using System.Security.Cryptography;

namespace TennisProjekt24.Services
{
    public class PracticeService : Connection, IPracticeService
    {
        private string _getAllPracticesString = $"SELECT PracticeId, Date, Title, Description, NoOfTrainings, MaxNoOfAteendees, InstructorId, Type FROM Practices";
        private string _getPracticeString = $"SELECT * FROM Practices WHERE PracticeID = @ID";
        private string _addPracticeString = $"INSERT INTO Practices VALUES(@Date, @Title, @Desc,  @NoTrain, @MaxAtendees,  @Type , @Instructor)";
        private string _deletePracticeString = $"DELETE FROM Practices WHERE PracticeId = @ID";
        private string _updatePracticeString = $"UPDATE Practices SET Date = @Date, Title = @Title, Description = @Desc NoOfTrainings = @NoTrain, " +
                                                "MaxNoOfAteendees = @MaxAtendees, Type = @Type , InstructorId = @Instructor WHERE PracticeId = @ID";

        private IInstructorService _instructorService = new InstructorService();

        /**
* return type: bool, which is determined by wheter the sql query changed exactly 1 row
* The method takes one parameter and has no overloads. The parameter is of the type Practice
* The purpose of the method is to add the Practice given as a parameter to a database via SqlCoomand
*/
        public bool AddPractice(Practice practice)
        {
            //In this line the Microsoft.Data.SqlClient is utilized to connect to the data base and establish a SQL query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //Here is the pre written query given to SqlCommand
                    SqlCommand command = new SqlCommand(_addPracticeString, connection);
                    //Here the data from the Practice parameter is feed into the query 
                    command.Parameters.AddWithValue("@Date", practice.StartDate);
                    command.Parameters.AddWithValue("@Title", practice.Title);
                    command.Parameters.AddWithValue("@Desc", practice.Description);
                    command.Parameters.AddWithValue("@NoTrain", practice.NoOfTrainings);
                    command.Parameters.AddWithValue("@MaxAtendees", practice.MaxNoOfAttendees);
                    command.Parameters.AddWithValue("@Instructor", practice.Instructor.InstructorId);
                    command.Parameters.AddWithValue("@Type", practice.Type);
                    //Here the connection gets established
                    command.Connection.Open();
                    //In this line the query gets executed and the result of that, the number of rows affected, gets saved in an int
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
            //Practice practice = GetPractice(id);
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

       

        public List<Practice> GetAllPractices(int? mId = null)
        {
            List<Practice> practices = new List<Practice>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = _getAllPracticesString;
                    if (mId != null)
                    {
                        sql += " WHERE PracticeId IN (SELECT PracticeId FROM PracticesMembers WHERE MemberId = @mId)";

                    }
                    SqlCommand commmand = new SqlCommand(sql, connection);
                    if (mId != null) 
                        commmand.Parameters.AddWithValue("@mId", mId);
                    commmand.Connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        int practiceId = reader.GetInt32("PracticeID");//.GetInt32(0);
                        DateTime date = reader.GetDateTime("Date");
                        string title = (string)reader["Title"];
                        string description = (string)reader["Description"];
                        int NoOfTrainings = reader.GetInt32("NoOfTrainings");
                        int MaxNoOfAteendees = reader.GetInt32("MaxNoOfAteendees");
                        Instructor Instructor = _instructorService.GetInstructor(reader.GetInt32("InstructorId"));
                        PracticeTypeEnum type = (PracticeTypeEnum)reader.GetInt32("Type");
                        Practice practice = new Practice(practiceId, date, title, description, NoOfTrainings, MaxNoOfAteendees, Instructor, type);
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
                        string description = (string)reader["Description"];
                        int NoOfTrainings = reader.GetInt32("NoOfTrainings");
                        int MaxNoOfAteendees = reader.GetInt32("MaxNoOfAteendees");
                        Instructor Instructor = _instructorService.GetInstructor(reader.GetInt32("InstructorId"));
                        PracticeTypeEnum type = (PracticeTypeEnum)reader.GetInt32("Type");
                        Practice practice = new Practice(practiceId, date, title, description, NoOfTrainings, MaxNoOfAteendees, Instructor, type);
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
                    command.Parameters.AddWithValue("@Desc", practice.Description);
                    command.Parameters.AddWithValue("@NoTrain", practice.NoOfTrainings);
                    command.Parameters.AddWithValue("@MaxAtendees", practice.MaxNoOfAttendees);
                    command.Parameters.AddWithValue("@Instructor", practice.Instructor.InstructorId);
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

        //MemberPractice
        private string _addMemberPractice = $"INSERT INTO PracticesMembers VALUES (@MID, @PID)";
        private string _getMemberPractice = $"SELECT * FROM PracticesMembers Where PracticeId = @PID ANS MemberId = @MID";
        private string _getAllMemberPractice = $"SELECT PracticeId, MemberId FROM PracticesMembers";
        private string _getMemberPracticeByPractice = $"SELECT * FROM PracticesMembers Where PracticeId = @PID";
        private string _getMemberPracticeByMember = $"SELECT * FROM PracticesMembers Where MemberId = @MID";

        //IMemberService _memberService = new MemberService();

        public bool AddMemberPractice(int memberId, int practiceId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addMemberPractice, connection);
                    command.Parameters.AddWithValue("@PID", practiceId);
                    command.Parameters.AddWithValue("@MID", memberId);
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

        //public (Member, Practice) GetMemberPractice(int memberId, int practiceId)
        //{
        //    Practice practice = GetPractice(practiceId);
        //    Member member = _memberService.GetMember(memberId);
        //    return (member, practice);
        //}

        public List<(int, int)> GetAllMemberPractices()
        {
            List<(int, int)> memberPractices = new List<(int, int)>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(_getAllMemberPractice, connection);
                    commmand.Connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        int practiceId = reader.GetInt32("PracticeID");//.GetInt32(0);
                        int memberId = reader.GetInt32("MemberId");
                        memberPractices.Add((memberId, practiceId));
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
            return memberPractices;
        }

        public List<(int, int)> GetAllMemberPracticesByMember(int memberId)
        {
            List<(int, int)> memberPractices = new List<(int, int)>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(_getMemberPracticeByMember, connection);
                    commmand.Parameters.AddWithValue("@MID", memberId);
                    commmand.Connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        int practiceId = reader.GetInt32("PracticeID");//.GetInt32(0);
                        //int memberId = reader.GetInt32("MemberId");
                        memberPractices.Add((memberId, practiceId));
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
            return memberPractices;
        }

        public List<(int, int)> GetAllMemberPracticesByPractice(int practiceId)
        {
            List<(int, int)> memberPractices = new List<(int, int)>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(_getMemberPracticeByPractice, connection);
                    commmand.Parameters.AddWithValue("@PID", practiceId);
                    commmand.Connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        //int practiceId = reader.GetInt32("PracticeID");//.GetInt32(0);
                        int memberId = reader.GetInt32("MemberId");
                        memberPractices.Add((memberId, practiceId));
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
            return memberPractices;
        }
    }
}
