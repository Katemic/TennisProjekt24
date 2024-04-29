using Microsoft.Data.SqlClient;
using System.Data;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services
{
    public class BookingService : Connection, IBookingService
    {
        private string _addBookingSQL = "INSERT INTO Bookings VALUES(@Date, @Duration, @MemberId, @SecondMember, @CourtId, @Type, @Note, @Time)";
        private string _getAllBookingsSQL = "SELECT BookingId, Date, Duration, MemberId, SecondMember, CourtId, Type,  Note, Time FROM Bookings";
        private string _checkAvailability = "SELECT * FROM Bookings WHERE CourtId = @CourtId AND Date = @Date AND Time = @Time";




        public bool AddBooking(Booking booking)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addBookingSQL, connection);
                    command.Parameters.AddWithValue("@Date",booking.Date);
                    command.Parameters.AddWithValue("@Duration", booking.Duration);
                    command.Parameters.AddWithValue("@MemberId", booking.MemberId);
                    command.Parameters.AddWithValue("@SecondMember", booking.SecondMember);
                    command.Parameters.AddWithValue("@CourtId", booking.Court);
                    command.Parameters.AddWithValue("@Type", booking.Type);
                    command.Parameters.AddWithValue("@Note", booking.Note);
                    command.Parameters.AddWithValue("@Time", booking.Time);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
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
        }

        public bool CheckAvailability(int courtId, DateOnly date, TimeOnly time)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                bool check = true;

                try
                {
                    SqlCommand command = new SqlCommand(_checkAvailability, connection);
                    command.Parameters.AddWithValue("@CourtId", courtId);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@Time", time);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) 
                    {
                        check = false;
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

                return check;
            }

        }

        public bool DeleteBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking> ();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand command = new SqlCommand(_getAllBookingsSQL, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read()) 
                    {
                        int bookingId = reader.GetInt32("BookingId");
                        //string dateParse = reader.GetDateTime("Date").Date.ToString();
                        DateTime datetime = reader.GetDateTime("Date");
                        DateOnly date = DateOnly.FromDateTime(datetime);
                        int duration = reader.GetByte("Duration");
                        int memberId = reader.GetInt32("MemberId");
                        int secondMember = reader.GetInt32("SecondMember");
                        int courtId = reader.GetInt32("CourtId");
                        BookingTypeEnum bookingType = (BookingTypeEnum)reader.GetInt32("Type");
                        string note = reader.GetString("Note");
                        //string timeParse = reader.GetDateTime("Time").Hour.ToString();
                        TimeSpan datetimeTime = (TimeSpan) reader["Time"];
                        TimeOnly time = TimeOnly.FromTimeSpan (datetimeTime);
                        Booking booking = new Booking(bookingId, date, time, duration, memberId, secondMember, courtId, bookingType, note);
                        bookings.Add(booking);
                    }

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

            return bookings;
        }

        public Booking GetBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetBookingsByCourt(int courtId)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetBookingsByMember(int memberId)
        {
            throw new NotImplementedException();
        }

        public bool updateBooking(Booking booking, int bookingId)
        {
            throw new NotImplementedException();
        }
    }
}
