using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TennisProjekt24.Interfaces;
using TennisProjekt24.Models;
using TennisProjekt24.Services;

namespace TennisProjekt24.Pages.Events
{
    public class IndexModel : PageModel
    {
        private IEventService _eventService;
        private IMemberService _memberService;

        public List<Event> events { get; set; }
        public Member CurrentMember { get; set; }

        public IndexModel(IEventService eventService, IMemberService memberService)
        {
            _eventService = eventService;
            _memberService = memberService;
        }
        public IActionResult OnGet()
        {
            //if (HttpContext.Session.GetInt32("MemberId") == null)
            //{
            //    return RedirectToPage("/Members/LogIn");
            //}
            //else
            //{
                //int sessionMemberId = (int)HttpContext.Session.GetInt32("MemberId");
                //CurrentMember = _memberService.GetMember(sessionMemberId);
                events = _eventService.GetAllEvents();
                return Page();
            //}

        }
    }
}
