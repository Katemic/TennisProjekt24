using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using TennisProjekt24.Helpers;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.BuddyForums
{
    public class IndexModel : PageModel
    {
        private IBuddyForumService _buddyForumService;
        //public IForumCommentService _forumCommentService;

        public List<BuddyForum> buddyForums { get; set; }
        [BindProperty(SupportsGet = true)]
        public SkillTypeEnum SkillLevel { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool IsEnabled { get; set; }

        public IndexModel(IBuddyForumService buddyForumService)
        {
            _buddyForumService = buddyForumService;
        }

        public void OnGet()
        {
            try
            {
                if ((int)SkillLevel > 0)
                {
                    buddyForums = _buddyForumService.GetBySkillPosts((int)SkillLevel);
                }
                else
                {
                    buddyForums = _buddyForumService.GetAllPosts();
                }
                if (IsEnabled)
                {
                    buddyForums = buddyForums.Where(b => b.Poster.MemberId == (int)HttpContext.Session.GetInt32("MemberId")).ToList();
                }
            }
            catch (SqlException sql)
            {
                ViewData["ErrorMessage"] = sql.Message;
            }
            catch (Exception ex)
            {
                buddyForums = new List<BuddyForum>();
                ViewData["ErrorMessage"] = ex.Message;
            }
        }
    }
}
