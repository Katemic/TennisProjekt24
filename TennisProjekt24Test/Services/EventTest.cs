using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24Test.Services
{
    [TestClass]
    public class EventTest
    {
        
        private MemberService _memberService;
        private EventService _eventService;
        public void Setup()
        {
            _memberService = new MemberService();
            _eventService = new EventService(_memberService);
        }
        [TestMethod]
        public void TestAddEvent()
        {
            
            Setup();
            Member m = new Member(10, "jonas", "jonas", "jonas", "jonas@jonas", "12345678", "TestAdress", "1234", MemberTypeEnum.Junior, true, "6a581b0d-92d5-45a7-8f18-a72dd97c28db_Tesla.jpg");
            int numberBefore = _eventService.GetAllEvents().Count();

            Event ev = new Event(1, new DateTime(2024, 7, 25, 18, 30, 0), "Grill", "Hygge", "Roskilde", m, "6a581b0d-92d5-45a7-8f18-a72dd97c28db_Tesla.jpg");
            _eventService.AddEvent(ev);

            int numberAfter = _eventService.GetAllEvents().Count();
            _eventService.DeleteEvent(_eventService.GetAllEvents().Last().EventId);

            Assert.AreEqual(numberBefore + 1, numberAfter);
        }
        [TestMethod]
        public void TestUpdateEvent()
        {

            Setup();
            Member m = new Member(10, "jonas", "jonas", "jonas", "jonas@jonas", "12345678", "TestAdress", "1234", MemberTypeEnum.Junior, true, "6a581b0d-92d5-45a7-8f18-a72dd97c28db_Tesla.jpg");
            int numberBefore = _eventService.GetAllEvents().Count();

            Event ev = new Event(1, new DateTime(2024, 7, 25, 18, 30, 0), "Grill", "Hygge", "Roskilde", m, "6a581b0d-92d5-45a7-8f18-a72dd97c28db_Tesla.jpg");
            _eventService.AddEvent(ev);

            Event ev2 = new Event(1, new DateTime(2024, 7, 25, 18, 30, 0), "GrillUpdate", "Hygge", "Roskilde", m, "6a581b0d-92d5-45a7-8f18-a72dd97c28db_Tesla.jpg");

            _eventService.UpdateEvent(_eventService.GetAllEvents().Last().EventId ,ev2);
            Event evTest = _eventService.GetEvent(_eventService.GetAllEvents().Last().EventId);
            _eventService.DeleteEvent(_eventService.GetAllEvents().Last().EventId);

            // evTest endte med at have et andet ID
            Assert.AreEqual(evTest.Title, ev2.Title);
        }
    }
}
