using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisProjekt24.Helpers;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24Test.Services
{

    [TestClass]
    public class MembersTest
    {
        //arrange
        private IMemberService _memberService = new MemberService();

        [TestMethod]
        public void AddMemberTest()
        {
            
            //act
            int numbersBefore = _memberService.GetAllMembers().Count;

            Member member = new Member("brugernavn", "password", "navn", "email", "number", "address", "0000", (MemberTypeEnum)1, false, "image");
            _memberService.AddMember(member);

            int numbersAfter = _memberService.GetAllMembers().Count;

            _memberService.DeleteMember(_memberService.GetAllMembers().Last().MemberId);

            //assert
            Assert.AreEqual(numbersBefore + 1, numbersAfter);

        }

        [TestMethod]
        public void DeleteMemberTest()
        {
            //act
            Member member = new Member("brugernavn", "password", "navn", "email", "number", "address", "0000", (MemberTypeEnum)1, false, "image");
            _memberService.AddMember(member);
            int numbersBefore = _memberService.GetAllMembers().Count;

            _memberService.DeleteMember(_memberService.GetAllMembers().Last().MemberId);
            int numbersAfter = _memberService.GetAllMembers().Count;

            //assert
            Assert.AreEqual(numbersBefore-1, numbersAfter);
        }


        [TestMethod]
        public void VerifyLoginTestFail()
        {
            //act
            Member member = new Member("brugernavn", "password", "navn", "email", "number", "address", "0000", (MemberTypeEnum)1, false, "image");
            _memberService.AddMember(member);

            Member login = _memberService.VerifyLogin("fakeUsername", "fakePassword");

            _memberService.DeleteMember(_memberService.GetAllMembers().Last().MemberId);

            //assert 
            Assert.IsNull(login);

        }

        [TestMethod]
        public void VerifyLoginTestSuccess()
        {
            //act
            Member member = new Member("brugernavn", "password", "navn", "email", "number", "address", "0000", (MemberTypeEnum)1, false, "image");
            _memberService.AddMember(member);

            Member login = _memberService.VerifyLogin("brugernavn", "password");

            _memberService.DeleteMember(_memberService.GetAllMembers().Last().MemberId);

            //assert 
            Assert.IsNotNull(login);

        }


    }
}
