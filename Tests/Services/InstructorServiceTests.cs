using Microsoft.VisualStudio.TestTools.UnitTesting;
using TennisProjekt24.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services.Tests
{
    [TestClass()]
    public class InstructorServiceTests
    {
        private IInstructorService _instructorService;
        private void Setup()
        {
            _instructorService = new InstructorService();
        }
        [TestMethod()]
        public void AddInstructorTest()
        {
            Setup();
            Instructor test = new Instructor(1, "Kim", "23242526", "Han er lige på en tester", "En flot fyr");
            Assert.IsTrue(_instructorService.AddInstructor(test));
        }

        [TestMethod()]
        public void DeleteInstructorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllInstructorsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetInstructorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateInstructorTest()
        {
            Assert.Fail();
        }
    }
}