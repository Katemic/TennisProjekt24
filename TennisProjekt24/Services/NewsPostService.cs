using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;
using System.Data;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Services
{
    public class NewsPostService : Connection, INewsPostService
    {

        private string _addPostSQL = "INSERT INTO NewsPosts VALUES (@Title, @Text, @Date, @MemberId)";
        private string _deletePostSQL = "DELETE FROM NewsPosts WHERE NewsPostId = @Id";
        private string _getAllPostsSQL = "SELECT NewsPostId, Title, Text, Date, MemberId FROM NewsPosts";
        private string _getPostSQL = "SELECT NewsPostId, Title, Text, Date, MemberId FROM NewsPosts WHERE NewsPostId = @Id";
        private string _updatePostSQL = "UPDATE NewsPosts SET Title = @Title, Text = @Text, Date = @Date, MemberId = @MemberId WHERE NewsPostId = @Id";


        private IMemberService _memberService;

        public NewsPostService(IMemberService memberService)
        {
            _memberService = memberService;
        }



        public bool AddPost(NewsPost newsPost)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addPostSQL, connection);
                    command.Parameters.AddWithValue("@Title", newsPost.Title);
                    command.Parameters.AddWithValue("@Text", newsPost.Text);
                    command.Parameters.AddWithValue("@Date", newsPost.Date);
                    command.Parameters.AddWithValue("@MemberId", newsPost.Member.MemberId);
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

        public bool DeletePost(int newsPostId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_deletePostSQL, connection);
                    command.Parameters.AddWithValue("@Id", newsPostId);
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



        public List<NewsPost> GetAllPosts()
        {
            List<NewsPost> posts = new List<NewsPost>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAllPostsSQL, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) 
                    {
                        int newsPostId = reader.GetInt32("NewsPostId");
                        string title = reader.GetString("Title");
                        string text = reader.GetString("Text");
                        DateTime date = reader.GetDateTime("Date");
                        int memberId = reader.GetInt32("MemberId");
                        Member member = _memberService.GetMember(memberId);
                        NewsPost newsPost = new NewsPost(newsPostId, title, text, date, member);
                        posts.Add(newsPost);
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

            return posts;

        }

        public NewsPost GetPost(int newsPostId)
        {
            NewsPost newsPost = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getPostSQL, connection);
                    command.Parameters.AddWithValue("@Id",newsPostId);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int postId = reader.GetInt32("NewsPostId");
                        string title = reader.GetString("Title");
                        string text = reader.GetString("Text");
                        DateTime date = reader.GetDateTime("Date");
                        int memberId = reader.GetInt32("MemberId");
                        Member member = _memberService.GetMember(memberId);
                        newsPost = new NewsPost(postId, title, text, date, member);
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

            return newsPost;
        }

        public bool UpdatePost(NewsPost newsPost, int newsPostId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_updatePostSQL, connection);
                    command.Parameters.AddWithValue("@Id", newsPostId);
                    command.Parameters.AddWithValue("@Title", newsPost.Title);
                    command.Parameters.AddWithValue("@Text", newsPost.Text);
                    command.Parameters.AddWithValue("@Date", newsPost.Date);
                    command.Parameters.AddWithValue("@MemberId", newsPost.Member.MemberId);
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
    }
}
