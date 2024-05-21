using Microsoft.Data.SqlClient;
using System.Data;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services
{
    public class CourtService : Connection, ICourtService
    {
        private string _getAllString = "SELECT * FROM Courts";
        private string _getByIdSql = "SELECT * FROM Courts WHERE CourtId=@CourtId";
        private string _insertSql = "INSERT INTO Courts VALUES(@CourtNumber, @Outdoor, @CourtType, @Availability)";
        private string _deleteSql = "DELETE FROM Courts WHERE CourtId=@CourtId";
        private string _updateSql = "UPDATE Courts SET Outdoor=@Outdoor, CourtNo=@CourtNumber, Type=@CourtType, Availability=@Availability WHERE CourtId=@CourtId";

        public bool AddCourt(Court court)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_insertSql, connection);
                    command.Parameters.AddWithValue("@Outdoor", court.Outdoor);
                    command.Parameters.AddWithValue("@CourtNumber", court.CourtNumber);
                    command.Parameters.AddWithValue("@CourtType", court.CourtType);
                    command.Parameters.AddWithValue("@Availability", court.Availability);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return false;
        }

        public bool DeleteCourt(int courtId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand deleteCommand = new SqlCommand(_deleteSql, connection);
                    deleteCommand.Parameters.AddWithValue("@CourtId", courtId);
                    deleteCommand.Connection.Open();
                    deleteCommand.ExecuteNonQuery();
                    deleteCommand.Connection.Open();
                    int noOfRows = deleteCommand.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return false;
        }

        public List<Court> GetAllCourts(string? filter = null)
        {
            List<Court> courts = new List<Court>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = _getAllString;
                    if (filter != null)
                    {
                        sql += " WHERE 1=1 " + filter;
                    }
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int courtId = reader.GetInt32("CourtId");
                        bool outdoor = reader.GetBoolean("Outdoor");
                        int courtNumber = reader.GetInt32("CourtNo");
                        CourtTypeEnum courtType = (CourtTypeEnum)reader.GetInt32("Type");
                        bool availability = reader.GetBoolean("Availability");
                        Court court = new Court(courtId, outdoor, courtNumber, courtType, availability);
                        courts.Add(court);
                    }
                    reader.Close();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return courts;
        }

        public Court GetCourt(int courtId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getByIdSql, connection);
                    command.Parameters.AddWithValue("@CourtId", courtId);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        bool outdoor = reader.GetBoolean("Outdoor");
                        int courtNumber = reader.GetInt32("CourtNo");
                        CourtTypeEnum courtType = (CourtTypeEnum)reader.GetInt32("Type");
                        bool availability = reader.GetBoolean("Availability");
                        Court court = new Court(courtId, outdoor, courtNumber, courtType, availability);
                        return court;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return null;
        }

        public bool UpdateCourt(int courtId, Court court)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateSql, connection);
                    command.Parameters.AddWithValue("@CourtId", courtId);
                    command.Parameters.AddWithValue("@Outdoor", court.Outdoor);
                    command.Parameters.AddWithValue("@CourtNumber", court.CourtNumber);
                    command.Parameters.AddWithValue("@CourtType", court.CourtType);
                    command.Parameters.AddWithValue("@Availability", court.Availability);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return false;
        }
    }
}
