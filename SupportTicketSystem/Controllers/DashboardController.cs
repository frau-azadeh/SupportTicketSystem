using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;

namespace SupportTicketSystem.Api
{
    [Route("api/dashboard")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
[HttpGet("admin")]
public async Task<IActionResult> GetAllTickets()
{
    var itUsers = await _context.Users
        .Where(u => u.Role == "IT")
        .Select(u => new
        {
            Id = u.Id,
            FullName = u.FullName
        })
        .ToListAsync();

    var tickets = await _context.Tickets
        .Include(t => t.CreatedByUser)
        .Include(t => t.AssignedToUser)
        .ToListAsync();

    var response = new
    {
        tickets = tickets.Select(t => new
        {
            t.Id,
            t.Title,
            UserName = t.CreatedByUser.FullName,
            Status = t.Status ?? "در حال بررسی",
            AssignedTo = t.AssignedToUser != null ? t.AssignedToUser.FullName : "هنوز ارجاع نشده",
            AssignedToId = t.AssignedToUserId,
        }),
        itUsers,
        summary = new
        {
            pending = tickets.Count(t => t.Status == null || t.Status == "در انتظار بررسی"),
            inprogress = tickets.Count(t => t.Status == "در حال انجام"),
            done = tickets.Count(t => t.Status == "انجام شده"),
            canceled = tickets.Count(t => t.Status == "باطل شده")
        }
    };

    return Ok(response);
}



        [HttpGet("it-users")]
        public async Task<IActionResult> GetITUsers()
        {
            var its = await _context.Users
                .Where(u => u.Role == "IT")
                .Select(u => new { u.Id, u.FullName })
                .ToListAsync();

            return Ok(its);
        }
        [HttpPost("assign")]
        public async Task<IActionResult> AssignTicket([FromBody] AssignDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(dto.TicketId);

            if (ticket == null)
                return NotFound("تیکت پیدا نشد");

            ticket.AssignedToUserId = dto.UserId;
            ticket.Status = "در حال انجام";

            await _context.SaveChangesAsync();
            return Ok(new { message = "تیکت با موفقیت ارجاع داده شد" });
        }

        public class AssignDto
        {
            public int TicketId { get; set; }
            public int UserId { get; set; }
        }

    }
}
