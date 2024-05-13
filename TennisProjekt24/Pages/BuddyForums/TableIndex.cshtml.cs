using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.BuddyForums
{
    public class TableIndexModel : PageModel
    {
        private IBuddyForumService _buddyForumService;

        public List<BuddyForum> buddyForums { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SkillLevel { get; set; }

        public TableIndexModel(IBuddyForumService buddyForumService)
        {
            _buddyForumService = buddyForumService;
        }

        public void OnGet()
        {
            try
            {
                if (SkillLevel == "Letÿvet")
                    buddyForums = _buddyForumService.GetBySkillPosts(1);
                else if (SkillLevel == "ÿvet")
                    buddyForums = _buddyForumService.GetBySkillPosts(2);
                else if (SkillLevel == "Rutineret")
                    buddyForums = _buddyForumService.GetBySkillPosts(3);
                else
                    buddyForums = _buddyForumService.GetAllPosts();

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
