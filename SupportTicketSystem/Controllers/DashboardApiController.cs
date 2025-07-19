using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Data;

namespace SupportTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("admin")]
        public IActionResult GetAdminStats()
        {
            var totalTickets = _context.Tickets.Count();
            var openTickets = _context.Tickets.Count(t => t.Status == "باز");
            var users = _context.Users.Count();

            return Ok(new
            {
                totalTickets,
                openTickets,
                users
            });
        }

        [HttpGet("it/{userId}")]
        public IActionResult GetITStats(int userId)
        {
            var assignedTickets = _context.Tickets.Count(t => t.AssignedToUserId == userId);
            return Ok(new
            {
                assignedTickets
            });
        }

        [HttpGet("employee/{userId}")]
        public IActionResult GetEmployeeStats(int userId)
        {
            var myTickets = _context.Tickets.Count(t => t.CreatedByUserId == userId);
            return Ok(new
            {
                myTickets
            });
        }
    }
}
