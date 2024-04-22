namespace TennisProjekt24.Models
{
    public class NewsPost
    {
        public int NewsPostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int MemberId { get; set; }


        public NewsPost()
        {
            
        }

    }
}
