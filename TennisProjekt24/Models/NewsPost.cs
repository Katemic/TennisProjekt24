namespace TennisProjekt24.Models
{
    public class NewsPost
    {
        public int NewsPostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        //public int MemberId { get; set; }

        public Member Member { get; set; }


        public NewsPost()
        {
            
        }

        //public NewsPost(int newsPostId, string title, string text, DateTime date, int memberId)
        //{
        //    NewsPostId = newsPostId;
        //    Title = title;
        //    Text = text;
        //    Date = date;
        //    MemberId = memberId;
             
        //}

        public NewsPost(int newsPostId, string title, string text, DateTime date, Member member)
        {
            NewsPostId = newsPostId;
            Title = title;
            Text = text;
            Date = date;
            Member = member;

        }

    }
}
