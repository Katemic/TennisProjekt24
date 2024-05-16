using Microsoft.Data.SqlClient;
using System.Data;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using static TennisProjekt24.Models.Member;

namespace TennisProjekt24.Services
{
    public class MemberService : Connection, IMemberService
    {

        private string _addMemberSQL = "INSERT INTO Members VALUES(@Username, @Password, @Name, @Email, @PhoneNo, @Address, @Postcode, @MemberType, @Admin, @Image)";
        private string _deleteMemberSQL = "DELETE FROM Members WHERE MemberId = @Id";
        private string _getAllMembersSQL = "SELECT MemberId, Username, Password, Name, Email, PhoneNo, Address, Postcode, MemberType, Admin, Image FROM Members";
        private string _getMemberSQL = "SELECT MemberId, Username, Password, Name, Email, PhoneNo, Address, Postcode, MemberType, Admin, Image FROM Members WHERE MemberId = @Id";
        private string _updateMemberSQL = "UPDATE Members SET Username = @Username, Password = @Password, Name = @Name, Email = @Email, PhoneNo = @PhoneNo, " +
            "Address = @Address, Postcode = @Postcode, MemberType = @MemberType, Admin = @Admin  " +
            "WHERE MemberId = @Id";
        private string _verifyLoginSQL = "SELECT MemberId FROM Members WHERE Username = @Username AND Password = @Password";
        private string _checkUsernameSQL = "SELECT * FROM Members WHERE Username = @Username";


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
                    command.Parameters.AddWithValue("@Image",member.Image);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("der var en database fejl: " + sqlEx.Message);

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
                    Console.WriteLine("der var en database fejl: " + sqlEx.Message);

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

        public List<Member> GetAllMembers(int? pId = null)
        {
            List<Member> members = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string sql = _getAllMembersSQL;
                    if(pId != null)
                    {
                        sql += " WHERE MemberId IN (SELECT MemberId FROM PracticesMembers WHERE PracticeId = @pId)";
                        
                    }
                    SqlCommand command = new SqlCommand(sql, connection);
                    if (pId != null)
                    {
                        command.Parameters.AddWithValue("@pId", pId);
                    }
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
                        MemberTypeEnum memberType = (MemberTypeEnum)reader.GetInt32("MemberType");
                        //MemberTypeEnum memberTypeEnum = (MemberTypeEnum) Enum.Parse(typeof(MemberTypeEnum), memberType);
                        //MemberTypeEnum membertype = Enum.TryParse(typeof(MemberTypeEnum), reader.GetString("MemberType")); 
                        bool admin = reader.GetBoolean("Admin");
                        string image = reader.GetString("Image");
                        Member member = new Member(memberId, username, password, name, email, phoneNo, address, postcode, memberType, admin, image);
                        members.Add(member);
                    }
                    reader.Close();

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("der var en database fejl: " + sqlEx.Message);

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
                    if (reader.Read())
                    {
                        int memberID = reader.GetInt32("MemberId");
                        string username = reader.GetString("Username");
                        string password = reader.GetString("Password");
                        string name = reader.GetString("Name");
                        string email = reader.GetString("Email");
                        string phoneNo = reader.GetString("PhoneNo");
                        string address = reader.GetString("Address");
                        string postcode = reader.GetString("PostCode");
                        MemberTypeEnum memberType = (MemberTypeEnum)reader.GetInt32("MemberType");
                        //MemberTypeEnum memberTypeEnum = (MemberTypeEnum)Enum.Parse(typeof(MemberTypeEnum), memberType);
                        //MemberTypeEnum membertype = Enum.TryParse(typeof(MemberTypeEnum), reader.GetString("MemberType")); 
                        bool admin = reader.GetBoolean("Admin");
                        string image = reader.GetString("Image");
                        member = new Member(memberID, username, password, name, email, phoneNo, address, postcode, memberType, admin, image);

                    }

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("der var en database fejl: " + sqlEx.Message);

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateMemberSQL, connection);
                    command.Parameters.AddWithValue("@Id", memberId);
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
                    Console.WriteLine("der var en database fejl: " + sqlEx.Message);

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


        public Member VerifyLogin(string username, string password)
        {

            Member member = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand(_verifyLoginSQL, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int memberID = reader.GetInt32("MemberId");

                        member = GetMember(memberID);

                    }


                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("der var en database fejl: " + sqlEx.Message);

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


        public bool CheckUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                bool check = true;

                try
                {
                    SqlCommand command = new SqlCommand(_checkUsernameSQL, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        check = false;
                    }
                    reader.Close();

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("der var en database fejl: " + sqlEx.Message);

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

                return check;
            }

            

        }

    }

}
