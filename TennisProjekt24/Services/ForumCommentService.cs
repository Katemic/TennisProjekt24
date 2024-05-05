using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Data;
using TennisProjekt24.Helpers;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services
{
    public class ForumCommentService : Connection, IForumCommentService
    {
        private string _insertSql = "INSERT INTO ForumComments VALUES(@PostId, @MemberId, @Text, @DateTime)";
        private string _getByIdSql = "SELECT * FROM ForumComments WHERE PostId=@PostId";
        private string _getCommentSql = "SELECT * FROM ForumComments WHERE CommentId=@CommentId";
        private string _deleteSql = "DELETE FROM ForumComments WHERE CommentId=@CommentId";
        private string _updateSql = "UPDATE ForumComments SET Text=@Text WHERE CommentId=@CommentId";



        private MemberService _memberService = new MemberService();

        public bool CreateComment(ForumComment forumComment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_insertSql, connection);
                    command.Parameters.AddWithValue("@PostId", forumComment.PostId);
                    command.Parameters.AddWithValue("@MemberId", forumComment.Commenter.MemberId);
                    command.Parameters.AddWithValue("@Text", forumComment.Text);
                    command.Parameters.AddWithValue("@DateTime", forumComment.DateTime);
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

        public bool DeleteComment(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand deleteCommand = new SqlCommand(_deleteSql, connection);
                    deleteCommand.Parameters.AddWithValue("@CommentId", commentId);
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

        public ForumComment GetComment(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getCommentSql, connection);
                    command.Parameters.AddWithValue("@CommentId", commentId);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int postId = reader.GetInt32("PostId");
                        Member commenter = _memberService.GetMember(reader.GetInt32("MemberId"));
                        string text = reader.GetString("Text");
                        DateTime dateTime = reader.GetDateTime("DateTime");
                        ForumComment comment = new ForumComment(commentId, postId, commenter, text, dateTime);
                        return comment;
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

        public List<ForumComment> GetPostComments(int postId)
        {
            List<ForumComment> comments = new List<ForumComment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getByIdSql, connection);
                    command.Parameters.AddWithValue("@PostId", postId);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int commentId = reader.GetInt32("CommentId");
                        Member commenter = _memberService.GetMember(reader.GetInt32("MemberId"));
                        string text = reader.GetString("Text");
                        DateTime dateTime = reader.GetDateTime("DateTime");
                        ForumComment comment = new ForumComment(commentId, postId, commenter, text, dateTime);
                        comments.Add(comment);
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
            return comments;
        }

        public bool UpdateComment(ForumComment forumComment, int commentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateSql, connection);
                    command.Parameters.AddWithValue("@CommentId", commentId);
                    command.Parameters.AddWithValue("@Text", forumComment.Text);
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
}
