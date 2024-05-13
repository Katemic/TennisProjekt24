using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.NewsPosts
{
    public class IndexModel : PageModel
    {

        private INewsPostService _newsPostService;
        private IMemberService _memberService;

        public List<NewsPost> posts { get; set; }

        public Member CurrentMember { get; set; }

        public IndexModel(INewsPostService newsPostService, IMemberService memberService)
        {
            _newsPostService = newsPostService;
            _memberService = memberService;
        }


        public void OnGet()
        {
            posts = _newsPostService.GetAllPosts();

            if (HttpContext.Session.GetInt32("MemberId") != null)
            {
                int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                CurrentMember = _memberService.GetMember(sessionMemberId);
            }
        }
    }
}
