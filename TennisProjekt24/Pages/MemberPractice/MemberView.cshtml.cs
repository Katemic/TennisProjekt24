using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;

namespace TennisProjekt24.Pages.MemberPractice
{
    public class MemberViewModel : PageModel
    {
        private IPracticeService _practiceService;
        [BindProperty]
        public List<Practice> Practices { get; set; }

        public void OnGet(int id)
        {
            Practices = _practiceService.GetAllPractices(id);
           

        }
        public MemberViewModel(IPracticeService serv)
        {
            _practiceService = serv;
        }

        
    }
}
