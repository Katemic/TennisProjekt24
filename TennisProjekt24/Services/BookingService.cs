using Microsoft.Data.SqlClient;
using System.Data;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TennisProjekt24.Services
{
    public class BookingService : Connection, IBookingService
    {
        private string _addBookingSQL = "INSERT INTO Bookings VALUES(@Date, @Duration, @MemberId, @SecondMember, @CourtId, @Type, @Note, @Time)";
        private string _getAllBookingsSQL = "SELECT BookingId, Date, Duration, MemberId, SecondMember, CourtId, Type, Note, Time FROM Bookings";
        private string _checkAvailabilitySQL = "SELECT * FROM Bookings WHERE CourtId = @CourtId AND Date = @Date AND Time = @Time";
        private string _deleteBookingSQL = "DELETE FROM Bookings WHERE BookingId = @Id";
        private string _getBookingSQL = "SELECT BookingId, Date, Duration, MemberId, SecondMember, CourtId, Type, Note, Time FROM Bookings WHERE BookingId = @Id";
        private string _getAllBookingsByDateSQL = "SELECT BookingId, Date, Duration, MemberId, SecondMember, CourtId, Type, Note, Time FROM Bookings WHERE Date = @Date";
        private string _getBookingsByMemberSQL = "SELECT BookingId, Date, Duration, MemberId, SecondMember, CourtId, Type, Note, Time FROM Bookings WHERE MemberId = @MemberId OR SecondMember = @MemberId";
        private string _updateBookingSQL = "UPDATE Bookings SET SecondMember = @SecondMemberId, Type = @Type, Note = @Note WHERE BookingId = @Id";

        private IMemberService _memberService;
        private ICourtService _courtService;

        public BookingService(IMemberService memberService, ICourtService courtService)
        {
             _memberService = memberService;
            _courtService = courtService;
        }


        public bool AddBooking(Booking booking)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addBookingSQL, connection);
                    command.Parameters.AddWithValue("@Date",booking.Date);
                    command.Parameters.AddWithValue("@Duration", booking.Duration);
                    command.Parameters.AddWithValue("@MemberId", booking.Member.MemberId);
                    command.Parameters.AddWithValue("@SecondMember", booking.SecondMemberFull.MemberId);
                    command.Parameters.AddWithValue("@CourtId", booking.Court.CourtId);
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
                    SqlCommand command = new SqlCommand(_checkAvailabilitySQL, connection);
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_deleteBookingSQL, connection);
                    command.Parameters.AddWithValue("@Id", bookingId);
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
                        int secondMemberId = reader.GetInt32("SecondMember");
                        int courtId = reader.GetInt32("CourtId");
                        BookingTypeEnum bookingType = (BookingTypeEnum)reader.GetInt32("Type");
                        string note = reader.GetString("Note");
                        //string timeParse = reader.GetDateTime("Time").Hour.ToString();
                        TimeSpan datetimeTime = (TimeSpan) reader["Time"];
                        TimeOnly time = TimeOnly.FromTimeSpan (datetimeTime);
                        Member member = _memberService.GetMember(memberId);
                        Member secondMember = _memberService.GetMember(secondMemberId);
                        Court court = _courtService.GetCourt(courtId);
                        Booking booking = new Booking(bookingId, date, time, duration, member, secondMember, court, bookingType, note);

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
            Booking booking = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand command = new SqlCommand(_getBookingSQL, connection);
                    command.Parameters.AddWithValue("@Id", bookingId);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int bookingID = reader.GetInt32("BookingId");
                        //string dateParse = reader.GetDateTime("Date").Date.ToString();
                        DateTime datetime = reader.GetDateTime("Date");
                        DateOnly date = DateOnly.FromDateTime(datetime);
                        int duration = reader.GetByte("Duration");
                        int memberId = reader.GetInt32("MemberId");
                        int secondMemberId = reader.GetInt32("SecondMember");
                        int courtId = reader.GetInt32("CourtId");
                        BookingTypeEnum bookingType = (BookingTypeEnum)reader.GetInt32("Type");
                        string note = reader.GetString("Note");
                        //string timeParse = reader.GetDateTime("Time").Hour.ToString();
                        TimeSpan datetimeTime = (TimeSpan)reader["Time"];
                        TimeOnly time = TimeOnly.FromTimeSpan(datetimeTime);
                        Member member = _memberService.GetMember(memberId);
                        Member secondMember = _memberService.GetMember(secondMemberId);
                        Court court = _courtService.GetCourt(courtId);
                        booking = new Booking(bookingId, date, time, duration, member, secondMember, court, bookingType, note);
                        
                        
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

            return booking;
        }

        public List<Booking> GetBookingsByDate(DateOnly date)
        {
            List<Booking> bookings = new List<Booking> ();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand command = new SqlCommand(_getAllBookingsByDateSQL, connection);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int bookingID = reader.GetInt32("BookingId");
                        //string dateParse = reader.GetDateTime("Date").Date.ToString();
                        DateTime datetime = reader.GetDateTime("Date");
                        DateOnly date2 = DateOnly.FromDateTime(datetime);
                        int duration = reader.GetByte("Duration");
                        int memberId = reader.GetInt32("MemberId");
                        int secondMemberId = reader.GetInt32("SecondMember");
                        int courtId = reader.GetInt32("CourtId");
                        BookingTypeEnum bookingType = (BookingTypeEnum)reader.GetInt32("Type");
                        string note = reader.GetString("Note");
                        //string timeParse = reader.GetDateTime("Time").Hour.ToString();
                        TimeSpan datetimeTime = (TimeSpan)reader["Time"];
                        TimeOnly time = TimeOnly.FromTimeSpan(datetimeTime);
                        Member member = _memberService.GetMember(memberId);
                        Member secondMember = _memberService.GetMember(secondMemberId);
                        Court court = _courtService.GetCourt(courtId);
                        Booking booking = new Booking(bookingID, date, time, duration, member, secondMember, court, bookingType, note);
                        
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

        public List<Booking> GetBookingsByCourt(int courtId)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetBookingsByMember(int memberId)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand command = new SqlCommand(_getBookingsByMemberSQL, connection);
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int bookingID = reader.GetInt32("BookingId");
                        //string dateParse = reader.GetDateTime("Date").Date.ToString();
                        DateTime datetime = reader.GetDateTime("Date");
                        DateOnly date2 = DateOnly.FromDateTime(datetime);
                        int duration = reader.GetByte("Duration");
                        int memberID = reader.GetInt32("MemberId");
                        int secondMemberId = reader.GetInt32("SecondMember");
                        int courtId = reader.GetInt32("CourtId");
                        BookingTypeEnum bookingType = (BookingTypeEnum)reader.GetInt32("Type");
                        string note = reader.GetString("Note");
                        //string timeParse = reader.GetDateTime("Time").Hour.ToString();
                        TimeSpan datetimeTime = (TimeSpan)reader["Time"];
                        TimeOnly time = TimeOnly.FromTimeSpan(datetimeTime);
                        Member member = _memberService.GetMember(memberID);
                        Member secondMember = _memberService.GetMember(secondMemberId);
                        Court court = _courtService.GetCourt(courtId);
                        Booking booking = new Booking(bookingID, date2, time, duration, member, secondMember, court, bookingType, note);

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

        public bool updateBooking(Booking booking, int bookingId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateBookingSQL, connection);
                    command.Parameters.AddWithValue("@Id", bookingId);
                    command.Parameters.AddWithValue("@SecondMemberId", booking.SecondMemberFull.MemberId);
                    command.Parameters.AddWithValue("@Type", booking.Type);
                    command.Parameters.AddWithValue("@Note", booking.Note);
                    command.Connection.Open();
                    int NoOfRows = command.ExecuteNonQuery();
                    return NoOfRows == 1;
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
    }
}
