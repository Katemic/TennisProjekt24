﻿using Microsoft.Data.SqlClient;
using System.Data;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using static TennisProjekt24.Models.Member;

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
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    SqlCommand command = new SqlCommand(AddMemberSQL, connection);
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
            throw new NotImplementedException();
        }

        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    SqlCommand command = new SqlCommand(GetAllMembersSQL, connection);
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
                        //MemberTypeEnum membertype = reader.GetString("MemberType"); ???
                        bool admin = reader.GetBoolean("Admin");
                        //udfyld constructor
                        //Member member = new Member();
                        //tilføj til liste 
                        //members.Add(member);
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
