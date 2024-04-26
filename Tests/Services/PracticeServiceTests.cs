using Microsoft.VisualStudio.TestTools.UnitTesting;
using TennisProjekt24.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services.Tests
{
    [TestClass()]
    public class PracticeServiceTests
    {
        private PracticeService service;
        private void Setup() { 
            service = new PracticeService();
        }
        [TestMethod()]
        public void AddPracticeTest()
        {
            Setup();
            Practice tester = new Practice(0, DateTime.Now, "Test fra unit", 4, 5, 1, PracticeTypeEnum.ForEveryone);
            Assert.IsTrue(service.AddPractice(tester));
            //Assert.Fail();
        }

        [TestMethod()]
        public void DeletePracticeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllPracticesTest()
        {
            Setup();
            Assert.AreEqual(8, service.GetAllPractices().Count);
        }

        [TestMethod()]
        public void GetPracticeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetPracticeByTypeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdatePracticeTest()
        {
            Setup();
            Practice update = new Practice(0, DateTime.Now, "Den kunne opdateres", 6, 10, 2, PracticeTypeEnum.Intermediate);
            Assert.IsTrue(service.UpdatePractice(update, 1019));
        }
    }
}