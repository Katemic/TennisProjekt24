using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IMemberService
    {

        bool AddMember(Member member);

        bool DeleteMember(int memberId);

        bool UpdateMember(Member member, int memberId);

        Member GetMember(Member member);

        List<Member> GetAllMembers();   




    }
}
