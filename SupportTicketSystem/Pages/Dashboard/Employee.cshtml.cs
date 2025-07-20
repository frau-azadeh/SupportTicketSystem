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

        public List<Ticket> UserTickets { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userIdStr = User.FindFirst("UserId")?.Value;
            if (int.TryParse(userIdStr, out int userId))
            {
                UserTickets = await _context.Tickets
                    .Where(t => t.CreatedByUserId == userId)
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();
            }
        }
    }
}
