using Microsoft.Data.SqlClient;
using System.Data;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using static TennisProjekt24.Models.Member;

namespace TennisProjekt24.Services
{
    public class MemberService : Connection, IMemberService
    {

        private string _addMemberSQL = "INSERT INTO Members VALUES(@Username, @Password, @Name, @Email, @PhoneNo, @Address, @Postcode, @MemberType, @Admin)";
        private string _deleteMemberSQL = "DELETE FROM Members WHERE MemberId = @Id";
        private string _getAllMembersSQL = "SELECT MemberId, Username, Password, Name, Email, PhoneNo, Address, Postcode, MemberType, Admin FROM Members";
        private string _getMemberSQL = "SELECT MemberId, Username, Password, Name, Email, PhoneNo, Address, Postcode, MemberType, Admin FROM Members WHERE MemberId = @Id";
        private string _updateMemberSQL = "UPDATE Members SET Username = @Username, Password = @Password, Name = @Name, Email = @Email, PhoneNo = @PhoneNo, " +
            "Address = @Address, Postcode = @Postcode, MemberType = @MemberType, Admin = @Admin  " +
            "WHERE MemberId = @Id";


        public bool AddMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addMemberSQL, connection);
                    command.Parameters.AddWithValue("@Username", member.Username);
                    command.Parameters.AddWithValue("@Password", member.Password);
                    command.Parameters.AddWithValue("@Name", member.Name);
                    command.Parameters.AddWithValue("@Email", member.Email);
                    command.Parameters.AddWithValue("@PhoneNo", member.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", member.Address);
                    command.Parameters.AddWithValue("@Postcode", member.PostCode);
                    command.Parameters.AddWithValue("@MemberType", member.MemberType);
                    command.Parameters.AddWithValue("@Admin", member.Admin);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("der var en database error: " + sqlEx.Message);

                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);

                    throw ex;
                }
                finally
                {

                }


            }
            
            
        }

        public bool DeleteMember(int memberId)
        {

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    SqlCommand command = new SqlCommand(_deleteMemberSQL, connection);
                    command.Parameters.AddWithValue("@Id", memberId);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("der var en database error: " + sqlEx.Message);

                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);

                    throw ex;
                }
                finally
                {

                }


            }

        }

        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAllMembersSQL, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) 
                    {
                        int memberId = reader.GetInt32("MemberId");
                        string username = reader.GetString("Username");
                        string password = reader.GetString("Password");
                        string name = reader.GetString("Name");
                        string email = reader.GetString("Email");
                        string phoneNo = reader.GetString("PhoneNo");
                        string address = reader.GetString("Address");
                        string postcode = reader.GetString("PostCode");
                        string memberType = reader.GetString("MemberType");
                        MemberTypeEnum memberTypeEnum = (MemberTypeEnum) Enum.Parse(typeof(MemberTypeEnum), memberType);
                        //MemberTypeEnum membertype = Enum.TryParse(typeof(MemberTypeEnum), reader.GetString("MemberType")); 
                        bool admin = reader.GetBoolean("Admin");
                        Member member = new Member(memberId,username,password,name,email,phoneNo,address,postcode,memberTypeEnum,admin);
                        members.Add(member);
                    }
                    reader.Close();

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("der var en database error: " + sqlEx.Message);

                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);

                    throw ex;
                }
                finally
                {

                }
            }

            return members;

        }

        public Member GetMember(int memberId)
        {
            Member member = null;
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                
                try
                {
                    SqlCommand command = new SqlCommand(_getMemberSQL, connection);
                    command.Parameters.AddWithValue("@Id", memberId);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) 
                    {
                        int memberID = reader.GetInt32("MemberId");
                        string username = reader.GetString("Username");
                        string password = reader.GetString("Password");
                        string name = reader.GetString("Name");
                        string email = reader.GetString("Email");
                        string phoneNo = reader.GetString("PhoneNo");
                        string address = reader.GetString("Address");
                        string postcode = reader.GetString("PostCode");
                        string memberType = reader.GetString("MemberType");
                        MemberTypeEnum memberTypeEnum = (MemberTypeEnum)Enum.Parse(typeof(MemberTypeEnum), memberType);
                        //MemberTypeEnum membertype = Enum.TryParse(typeof(MemberTypeEnum), reader.GetString("MemberType")); 
                        bool admin = reader.GetBoolean("Admin");
                        member = new Member(memberID, username, password, name, email, phoneNo, address, postcode, memberTypeEnum, admin);

                    }

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("der var en database error: " + sqlEx.Message);

                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);

                    throw ex;
                }
                finally
                {

                }


            }
            return member;
        }

        public bool UpdateMember(Member member, int memberId)
        {
            throw new NotImplementedException();
        }
    }
}
