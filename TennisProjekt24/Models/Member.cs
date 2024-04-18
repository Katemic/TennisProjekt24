namespace TennisProjekt24.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public MemberTypeEnum MemberType { get; set; }
        public bool Admin { get; set; } 

        public enum MemberTypeEnum
        {
            Senior, YoungSenior, Junior, Family, Passive
        }

        public Member()
        {
             
        }

    }
}
