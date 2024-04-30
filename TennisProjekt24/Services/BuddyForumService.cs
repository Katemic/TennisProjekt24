using Microsoft.Data.SqlClient;
using System.Data;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services
{
    public class BuddyForumService : Connection, IBuddyForumService
    {
        private string _getAllString = "SELECT * FROM BuddyForums";
        private string _insertSql = "INSERT INTO BuddyForums VALUES(@DateTime, @MemberId, @Title, @Text, @SkillType)";
        private string _deleteSql = "DELETE FROM BuddyForums WHERE PostId=@PostId";
        private string _getByIdSql = "SELECT * FROM BuddyForums WHERE PostId=@PostId";
        private string _updateSql = "UPDATE BuddyForums SET DateTime=@DateTime, Title=@Title, Text=@Text WHERE PostId=@PostId";

        public bool CreatePost(BuddyForum post)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_insertSql, connection);
                    command.Parameters.AddWithValue("@DateTime", post.DateTime);
                    command.Parameters.AddWithValue("@MemberId", post.MemberId);
                    command.Parameters.AddWithValue("@Title", post.Title);
                    command.Parameters.AddWithValue("@Text", post.Text);
                    command.Parameters.AddWithValue("@SkillType", post.SkillType);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return false;
        }

        public bool DeletePost(int postId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand deleteCommand = new SqlCommand(_deleteSql, connection);
                    deleteCommand.Parameters.AddWithValue("@PostId", postId);
                    deleteCommand.Connection.Open();
                    deleteCommand.ExecuteNonQuery();
                    deleteCommand.Connection.Open();
                    int noOfRows = deleteCommand.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return false;
        }

        public List<BuddyForum> GetAllPosts()
        {
            List<BuddyForum> posts = new List<BuddyForum>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAllString, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int postId = reader.GetInt32("PostId");
                        DateTime dateTime = reader.GetDateTime("DateTime");
                        string title = reader.GetString("Title");
                        string text = reader.GetString("Text");
                        int memberId = reader.GetInt32("MemberId");
                        SkillTypeEnum skillType = (SkillTypeEnum)reader.GetInt32("SkillType");
                        BuddyForum post = new BuddyForum(postId, dateTime, memberId, title, text, skillType);
                        posts.Add(post);
                    }
                    reader.Close();
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return posts;
        }

        public BuddyForum GetPostById(int postId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getByIdSql, connection);
                    command.Parameters.AddWithValue("@PostId", postId);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        DateTime dateTime = reader.GetDateTime("DateTime");
                        string title = reader.GetString("Title");
                        string text = reader.GetString("Text");
                        int memberId = reader.GetInt32("MemberId");
                        SkillTypeEnum skillType = (SkillTypeEnum)reader.GetInt32("SkillType");
                        BuddyForum post = new BuddyForum(postId, dateTime, memberId, title, text, skillType);
                        return post;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return null;
        }

        public bool UpdatePost(int postId, BuddyForum post)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateSql, connection);
                    command.Parameters.AddWithValue("@PostId", postId);
                    command.Parameters.AddWithValue("@DateTime", post.DateTime);
                    command.Parameters.AddWithValue("@Title", post.Title);
                    command.Parameters.AddWithValue("@Text", post.Text);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return false;
        }
    }
}
