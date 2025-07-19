using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupportTicketSystem.Data;
using SupportTicketSystem.Models;

namespace SupportTicketSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }  // ✅ تغییر از Email به Username

        [BindProperty]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPost()
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user == null)
            {
                ErrorMessage = "❌ اطلاعات ورود اشتباه است.";
                return Page();
            }

            // ذخیره اطلاعات در سشن
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("Role", user.Role ?? "");

            return RedirectToPage("/Dashboard"); 
        }
    }
}
