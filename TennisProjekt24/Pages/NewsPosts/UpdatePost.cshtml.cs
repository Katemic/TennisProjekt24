using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.NewsPosts
{
    public class UpdatePostModel : PageModel
    {

        private INewsPostService _newsPostService;


        [BindProperty]
        public NewsPost UpdatedPost { get; set; }

        public UpdatePostModel(INewsPostService newsPostService)
        {
             _newsPostService = newsPostService;
        }

        public void OnGet(int id)
        {
            UpdatedPost = _newsPostService.GetPost(id);
        }

        public IActionResult OnPost() 
        {
            
            //if (!ModelState.IsValid) 
            //{
            //    return Page();
            //}

            _newsPostService.UpdatePost(UpdatedPost, UpdatedPost.NewsPostId);
            return RedirectToPage("Index");
        }
    }
}
