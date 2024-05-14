using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IBookingService
    {

        bool AddBooking(Booking booking);

        bool DeleteBooking(int bookingId);

        bool updateBooking(Booking booking, int bookingId); //måske hvis man vil skifte makker 

        List<Booking> GetAllBookings(string? filter = null);

        Booking GetBooking(int bookingId);

        bool CheckAvailability(int courtId, DateOnly date, TimeOnly time);
        
        List<Booking> GetBookingsByCourt(int courtId);

        List<Booking> GetBookingsByMember(int memberId);

        List<Booking> GetBookingsByDate(DateOnly date);

        


    }
}
