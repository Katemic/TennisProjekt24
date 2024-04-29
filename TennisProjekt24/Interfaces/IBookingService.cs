using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IBookingService
    {

        bool AddBooking(Booking booking);

        bool DeleteBooking(int bookingId);

        bool updateBooking(Booking booking, int bookingId); //måske hvis man vil skifte makker 

        List<Booking> GetAllBookings();

        Booking GetBooking(int bookingId);

        bool CheckAvailability(int courtId, DateOnly date, TimeOnly time);
        
        List<Booking> GetBookingsByCourt(int courtId);

        List<Booking> GetBookingsByMember(int memberId);

        //noget med at checke om man har booket for mange timer


    }
}
