using Azure.Identity;

namespace TennisProjekt24.Models
{
    public enum MemberTypeEnum
        {
            Senior, YoungSenior, Junior, Family, Passive
        }

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

        
        public Member()
        {
             
        }

        public Member(int memberId, string username, string password, string name, string email, string phoneNumber, 
            string address, string postcode, MemberTypeEnum memberType, bool admin)
        {
            MemberId = memberId;
            Username = username;
            Password = password;
            Name = name; 
            Email = email; 
            PhoneNumber = phoneNumber;
            Address = address;
            PostCode = postcode;
            MemberType = memberType;
            Admin = admin;


        }

    }
}
