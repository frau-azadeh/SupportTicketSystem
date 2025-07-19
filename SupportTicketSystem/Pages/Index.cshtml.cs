using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SupportTicketSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToPage("/Login");
            }

            return Page();
        }

    }
}
