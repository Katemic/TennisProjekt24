using System.Transactions;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;


namespace TennisProjekt24Test.Services
{
    [TestClass]
    public class CourtTest
    {
        ICourtService _courtService = new CourtService();
        [TestMethod]
        public void TestAddCourt()
        {
            int numberBefore = _courtService.GetAllCourts().Count();

            Court court = new Court(false, 2, CourtTypeEnum.Padel, true);
            _courtService.AddCourt(court);

            int numberAfter = _courtService.GetAllCourts().Count();
            _courtService.DeleteCourt(_courtService.GetAllCourts().Last().CourtId);

            Assert.AreEqual(numberBefore + 1, numberAfter);
        }
        [TestMethod]
        public void TestDeleteCourt() 
        {
            Court court = new Court(false, 2, CourtTypeEnum.Padel, true);
            _courtService.AddCourt(court);
            int numberBefore = _courtService.GetAllCourts().Count();

            _courtService.DeleteCourt(_courtService.GetAllCourts().Last().CourtId);;
            int numberAfter = _courtService.GetAllCourts().Count();

            Assert.AreEqual(numberBefore - 1, numberAfter);
        }
        [TestMethod]
        public void TestUpdateCourt() 
        {
            Court court = new Court(false, 2, CourtTypeEnum.Padel, true);
            _courtService.AddCourt(court);

            Court newCourt = new Court(_courtService.GetAllCourts().Last().CourtId, true, 1, CourtTypeEnum.PickleBall, false);

            _courtService.UpdateCourt(_courtService.GetAllCourts().Last().CourtId, newCourt);
            Court testCourt = _courtService.GetAllCourts().Last();

            _courtService.DeleteCourt(_courtService.GetAllCourts().Last().CourtId);
            Assert.AreEqual(testCourt, newCourt);
        }
    }
}