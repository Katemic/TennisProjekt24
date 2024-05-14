using System.ComponentModel.DataAnnotations;
using TennisProjekt24.Helpers;

namespace TennisProjekt24.Models
{
    public class BuddyForum
    {
        public int PostId { get; set; }
        public DateTime DateTime { get; set; }
        public Member? Poster { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        [Range(1, 100, ErrorMessage = "Erfaringsniveau er påkrævet")]
        public SkillTypeEnum SkillType { get; set; }

        public BuddyForum()
        {
            
        }

        public BuddyForum(int postId, DateTime dateTime, Member poster, string title, string text, SkillTypeEnum skillType)
        {
            PostId = postId;
            DateTime = dateTime;
            Poster = poster;
            Title = title;
            Text = text;
            SkillType = skillType;
        }
    }
}
