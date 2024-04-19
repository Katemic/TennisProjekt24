using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services
{
    public class MemberService : Connection, IMemberService
    {

        private string AddMemberSQL = "INSERT INTO Members VALUES(@Username, @Password, @Name, @Email, @PhoneNo, @Address, @Postcode, @MemberType, @Admin)";
        private string DeleteMemberSQL = "DELETE FROM Members WHERE MemberId = @Id";
        private string GetAllMembersSQL = "SELECT MemberId, Username, Password, Name, Email, PhoneNo, Address, Postcode, MemberType, Admin FROM Members";
        private string GetMemberSQL = "SELECT MemberId, Username, Password, Name, Email, PhoneNo, Address, Postcode, MemberType, Admin FROM Members WHERE MemberId = @Id";
        private string UpdateMemberSQL = "";


        public bool AddMember(Member member)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMember(int memberId)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetAllMembers()
        {
            throw new NotImplementedException();
        }

        public Member GetMember(Member member)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMember(Member member, int memberId)
        {
            throw new NotImplementedException();
        }
    }
}
