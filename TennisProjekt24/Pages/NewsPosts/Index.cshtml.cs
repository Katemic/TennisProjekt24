using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.NewsPosts
{
    public class IndexModel : PageModel
    {

        private INewsPostService _newsPostService;

        public List<NewsPost> posts { get; set; }


        public IndexModel(INewsPostService newsPostService)
        {
             _newsPostService = newsPostService;
        }


        public void OnGet()
        {
            posts = _newsPostService.GetAllPosts();
        }
    }
}
