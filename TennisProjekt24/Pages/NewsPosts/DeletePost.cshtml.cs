using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.NewsPosts
{
    public class DeletePostModel : PageModel
    {
        private INewsPostService _newsPostService;

        public NewsPost PostToDelete { get; set; }

        public DeletePostModel(INewsPostService newsPostService)
        {
            _newsPostService = newsPostService;
        }


        public void OnGet(int id)
        {
            PostToDelete = _newsPostService.GetPost(id);
        }


        public IActionResult OnPost(int id) 
        {
            _newsPostService.DeletePost(id);
            return RedirectToPage("index");
        
        }

    }
}
