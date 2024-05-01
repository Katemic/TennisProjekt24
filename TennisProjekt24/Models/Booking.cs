using TennisProjekt24.Interfaces;
using TennisProjekt24.Services;

namespace TennisProjekt24.Models
{

    public enum BookingTypeEnum
    {
        Member, Event, Practice
    }

    public class Booking
    {
        public int BookingId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int Duration { get; set; }
        public int MemberId { get; set; }
        public int SecondMember { get; set; }
        public int Court { get; set; }
        public BookingTypeEnum Type { get; set; }
        public string Note { get; set; }

        public Member? Member { get; set; }
        public Member? SecondMemberFull { get; set; }


        public Booking(int bookingId, DateOnly date, TimeOnly time, int duration, int memberId,
            int secondMember, int court, BookingTypeEnum type, string note)
        {
            BookingId = bookingId;
            Date = date;
            Time = time;
            Duration = duration;
            MemberId = memberId;
            SecondMember = secondMember;
            Court = court;
            Type = type;
            Note = note;


        }

        public Booking(int bookingId, DateOnly date, TimeOnly time, int duration, Member member,Member secondMember, 
            int court, BookingTypeEnum type, string note)
        {
            BookingId = bookingId;
            Date = date;
            Time = time;
            Duration = duration;
            Member = member;
            SecondMemberFull = secondMember;
            Court = court;
            Type = type;
            Note = note;


        }

        public Booking()
        {
            
        }



        //public enum BookingTypeEnum
        //{
        //    Member, Event, Practice
        //}

    }
}
