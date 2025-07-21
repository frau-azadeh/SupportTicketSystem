using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Models;
using System.Security.Claims;

namespace SupportTicketSystem.Pages.Dashboard
{
    public class EmployeeModel : PageModel
    {
        private readonly AppDbContext _context;

        public EmployeeModel(AppDbContext context)
        {
            _context = context;
        }

        // Holds the list of tickets for the current user
        public List<Ticket> UserTickets { get; set; } = new();

        // This will be shown instead of "پنل کاربر"
        public string DisplayName { get; set; }

        public async Task OnGetAsync()
        {
            var userIdStr = User.FindFirst("UserId")?.Value;

            if (int.TryParse(userIdStr, out int userId))
            {
                // Load the tickets for the current user
                UserTickets = await _context.Tickets
                    .Where(t => t.CreatedByUserId == userId)
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();

                // Try to get user's full name from the Users table
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == userId);

                // If found, set DisplayName to full name; fallback to username if not
                DisplayName = user?.FullName ?? User.Identity?.Name ?? "کاربر";
            }
            else
            {
                DisplayName = "کاربر"; // Default fallback
            }
        }
    }
}
