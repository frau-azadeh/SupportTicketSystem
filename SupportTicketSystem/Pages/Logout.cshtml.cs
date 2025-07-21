// Pages/Dashboard/Logout.cshtml.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SupportTicketSystem.Pages.Dashboard
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync(); // لاگ‌اوت از کوکی
            return RedirectToPage("/Login");  // هدایت به صفحه لاگین
        }
    }
}
