using Azure.Identity;
using System.ComponentModel.DataAnnotations;

namespace TennisProjekt24.Models
{
    public enum MemberTypeEnum
        {
            Senior, YoungSenior, Junior, Family, Passive
        }

    public class Member
    {
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Udfyld brugernavn")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Udfyld password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Indtast navn")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Indtast email")]
        [EmailAddress(ErrorMessage ="Indtast gyldig email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Indtast telefonnummer")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Indtast adresse")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Indtast postnummer")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "vælg medlemstype")]
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
