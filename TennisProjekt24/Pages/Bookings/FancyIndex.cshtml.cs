using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Reflection.Metadata;
using TennisProjekt24.Helpers;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TennisProjekt24.Pages.Bookings
{
    public class FancyIndexModel : PageModel
    {

        public IBookingService _bookingService;

        private ICourtService _courtService;

        private IMemberService _memberService;

        [BindProperty(SupportsGet = true)]
        public DateOnly Date { get; set; }

        public Member CurrentMember { get; set; }

        public List<Court> Courts { get; set; }

        public List<Booking> Bookings { get; set; }

        //public List<TimeOnly> TimeOnly = new List<TimeOnly> { new TimeOnly(8,00), new TimeOnly(9,00), new TimeOnly(10,00) };

        public List<TimeOnly> TimeOnly = new TimeOnlyGenerator().TimeOnlyList;

        [BindProperty(SupportsGet = true)]
        public string TypeFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OutdoorFilter { get; set; }

        public List<string> TypeChoice = new List<string> { "Alle", "Tennis", "Paddel" };

        public List<string> OutdoorChoice = new List<string> { "Alle", "Udendørs", "Indendørs" };

        public FancyIndexModel(IBookingService bookingService, ICourtService courtService, IMemberService memberService)
        {
            _bookingService = bookingService;
            _courtService = courtService;
            _memberService = memberService;
        }



        public void OnGet()
        {


            string sql = "";
            if (TypeFilter == "Tennis")
            {
                sql += "AND Type = 2 ";
            }
            if (TypeFilter == "Paddel")
            {
                sql += "AND Type = 1 ";
            }
            if (OutdoorFilter == "Udendørs")
            {
                sql += "AND Outdoor = 1 ";
            }
            if (OutdoorFilter == "Indendørs")
            {
                sql += "AND Outdoor = 0 ";
            }

            if (sql.Length == 0)
            {
                Courts = _courtService.GetAllCourts();
            }
            else
            {
                Courts = _courtService.GetAllCourts(sql);
            }


            if (Date.Equals(new DateOnly(1,1,1)))
            {
                Date = DateOnly.FromDateTime(DateTime.Now);
                Bookings = _bookingService.GetBookingsByDate(Date);
            }
            else 
            {
                Bookings = _bookingService.GetBookingsByDate(Date);
            }

            if (HttpContext.Session.GetInt32("MemberId") != null)
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
            }



            //Courts = _courtService.GetAllCourts();
            //Bookings = _bookingService.GetAllBookings();

        }

      
     


        //public List<T> Filter<T>(List<T> listToFilter,List<Predicate<T>> filterConditions)
        //{
           
        //}



    }
}
