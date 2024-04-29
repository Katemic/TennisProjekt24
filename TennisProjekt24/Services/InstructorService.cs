using System.Data;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services
{
    public class InstructorService : Connection, IInstructorService

    {
        private string _addInstructorString = $"INSERT INTO Instructors VALUES(@Name, @Phone, @Desc, @Img)";
        private string _getAllInstructorsString = $"SELECT InstructorId, Name, PhoneNo, Description, Image FROM Instructors";
        private string _getInstructorString = $"SELECT InstructorId, Name, PhoneNo, Description, Image FROM Instructors WHERE InstructorId = @ID";
        private string _deleteInstructorString = $"DELETE FROM Instructors WHERE InstructorId = @ID";
        private string _updateInstructorString = $"UPDATE Instructors";
        public bool AddInstructor(Instructor instructor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addInstructorString, connection);
                    command.Parameters.AddWithValue("@Name", instructor.Name);
                    command.Parameters.AddWithValue("@Phone", instructor.PhoneNumber);
                    command.Parameters.AddWithValue("@Desc", instructor.Description);
                    command.Parameters.AddWithValue("@Img", instructor.Image);
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

        public bool DeleteInstructor(int instructorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(_deleteInstructorString, connection))
                {
                    command.Parameters.AddWithValue("@ID", instructorId);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
            }
            return false;
        }

        public List<Instructor> GetAllInstructors()
        {
            List<Instructor> instructors = new List<Instructor>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(_getAllInstructorsString, connection);
                    commmand.Connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("InstructorID");//.GetInt32(0);
                        string name = (string)reader["Name"];
                        string phone = (string)reader["PhoneNo"];
                        string desc = (string)reader["Description"];
                        string img = (string)reader["Image"];
                        Instructor instructor = new Instructor(id, name, phone, desc, img);
                        instructors.Add(instructor);
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
            return instructors;
        }

        public Instructor GetInstructor(int instructorId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(_getInstructorString, connection);
                    commmand.Parameters.AddWithValue("@ID", instructorId);
                    commmand.Connection.Open();

                    SqlDataReader reader = commmand.ExecuteReader();
                    if (reader.Read())
                    {
                        int id = reader.GetInt32("InstructorID");//.GetInt32(0);
                        string name = (string)reader["Name"];
                        string phone = (string)reader["PhoneNo"];
                        string desc = (string)reader["Description"];
                        string img = (string)reader["Image"];
                        Instructor instructor = new Instructor(id, name, phone, desc, img);
                        return instructor;
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

        public bool UpdateInstructor(Instructor instructor, int instructorId)
        {
            throw new NotImplementedException();
        }
    }
}
