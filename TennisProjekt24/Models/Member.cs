using Azure.Identity;
using System.ComponentModel.DataAnnotations;
using TennisProjekt24.Helpers;

namespace TennisProjekt24.Models
{
    //public enum MemberTypeEnum
    //    {
    //        none, Senior, YoungSenior, Junior, Family, Passive
    //    }

    public class Member
    {
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Udfyld brugernavn")]
        [StringLength(30,ErrorMessage ="Brugernavn kan ikke være længere end 30 karakterer")] 
        public string Username { get; set; }

        [Required(ErrorMessage ="Udfyld password")]
        [StringLength(30, ErrorMessage = "Password kan ikke være længere end 30 karakterer")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Indtast navn")]
        [StringLength(50, ErrorMessage = "Navn kan ikke være længere end 50 karakterer")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Indtast email")]
        [EmailAddress(ErrorMessage ="Indtast gyldig email")] //muligvis regulært udtryk i stedet 
        [StringLength(50, ErrorMessage = "Email kan ikke være længere end 50 karakterer")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Indtast telefonnummer")]
        [StringLength(11, ErrorMessage = "Telefon nummer skal være mellem 8 og 11 karakterer", MinimumLength = 8)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Indtast adresse")]
        [StringLength(50, ErrorMessage = "Adressen kan ikke være længere end 50 karakterer")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Indtast postnummer")]
        [StringLength(4, ErrorMessage = "Post nummer skal være 4 tegn",MinimumLength = 4)]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "vælg medlemstype")]
        [Range(1, 20, ErrorMessage = "vælg medlemstype")]
        public MemberTypeEnum MemberType { get; set; }
        
        public bool Admin { get; set; }

        public string? Image {  get; set; }
        
        public Member()
        {
             
        }

        public Member(int memberId, string username, string password, string name, string email, string phoneNumber, 
            string address, string postcode, MemberTypeEnum memberType, bool admin, string image)
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
            Image = image;


        }

        public Member(string username, string password, string name, string email, string phoneNumber,
            string address, string postcode, MemberTypeEnum memberType, bool admin, string image)
        {
            
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            PostCode = postcode;
            MemberType = memberType;
            Admin = admin;
            Image = image;


        }


        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            else if (!(obj is Member)) return false;

            else if (((Member)obj).MemberId == this.MemberId) return true;

            return false;
        }

    }
}
