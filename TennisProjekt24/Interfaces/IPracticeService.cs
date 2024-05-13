using TennisProjekt24.Models;

namespace TennisProjekt24.Interfaces
{
    public interface IPracticeService
    {
        Practice GetPractice(int id);
        bool AddPractice(Practice practice);
        bool DeletePractice(int id);
        bool UpdatePractice(Practice practice, int id);
        List<Practice> GetAllPractices(int? mId);
        List<Practice> GetPracticeByType();

        //MemberPractice
        //(Member, Practice) GetMemberPractice(int memberId, int practiceId);
        //List<(int, int)> GetAllMemberPractices();
        //List<(int, int)> GetAllMemberPracticesByPractice(int practiceId);
        //List<(int, int)> GetAllMemberPracticesByMember(int memberId);
        bool AddMemberPractice(int memberId, int practiceId);



    }
}
