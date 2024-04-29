using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.NewsPosts
{
    public class AddPostModel : PageModel
    {

        private INewsPostService _newsPostService;

        [BindProperty]
        public NewsPost NewPost { get; set; }

        public AddPostModel(INewsPostService newsPostService)
        {
            _newsPostService = newsPostService;
        }


        public void OnGet()
        {
        }


        public IActionResult OnPost() 
        {
            if(!ModelState.IsValid) 
            {
                return Page();
            }

            _newsPostService.AddPost(NewPost);
            return RedirectToPage("Index");
        
        }
    }
}
