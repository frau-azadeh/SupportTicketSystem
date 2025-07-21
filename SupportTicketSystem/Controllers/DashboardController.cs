// DashboardController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Models;

namespace SupportTicketSystem.Api
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTickets()
        {
            var itUsers = await _context.Users
                .Where(u => u.Role == "IT")
                .Select(u => new { u.Id, u.FullName })
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
                    Status = t.Status ?? "در انتظار بررسی",
                    AssignedTo = t.AssignedToUser != null ? t.AssignedToUser.FullName : "هنوز ارجاع نشده",
                    AssignedToId = t.AssignedToUserId
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

        [HttpGet("it")]
        [Authorize(Roles = "IT")]
        public async Task<IActionResult> GetITTickets()
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);

            var tickets = await _context.Tickets
                .Include(t => t.CreatedByUser)
                .Where(t => t.AssignedToUserId == userId)
                .ToListAsync();

            var response = tickets.Select(t => new
            {
                t.Id,
                t.Title,
                t.Description,
                t.Priority,
                t.Status,
                UserName = t.CreatedByUser.FullName,
                FileUrl = !string.IsNullOrEmpty(t.AttachmentPath)
                    ? $"/uploads/{t.AttachmentPath}"
                    : null
            });

            return Ok(response);
        }

        [HttpPost("assign/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignTicket(int ticketId, [FromBody] AssignDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);

            if (ticket == null)
                return NotFound("تیکت پیدا نشد");

            ticket.AssignedToUserId = dto.UserId;
            ticket.Status = "در حال انجام";

            await _context.SaveChangesAsync();
            return Ok(new { message = "تیکت با موفقیت ارجاع داده شد" });
        }


        [HttpPut("/api/tickets/{id}/status")]
        [Authorize(Roles = "IT")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusDto dto)
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            var user = await _context.Users.FindAsync(userId);

            var ticket = await _context.Tickets
                .Include(t => t.CreatedByUser)
                .Where(t => t.AssignedToUserId == userId && t.Id == id)
                .FirstOrDefaultAsync();

            if (ticket == null)
                return NotFound("تیکت یافت نشد یا اجازه دسترسی ندارید.");

            ticket.Status = dto.Status;

            // ساخت نوتیف برای کاربر ایجاد کننده
            var notif = new Notification
            {
                TicketId = ticket.Id,
                UserId = ticket.CreatedByUserId,
                Message = $"تیکت «{ticket.Title}» توسط کارشناس {user.FullName} {dto.Status} شد.",
                SenderName = user.FullName,
                CreatedAt = DateTime.Now
            };

            _context.Notifications.Add(notif);
            await _context.SaveChangesAsync();

            return Ok(new { message = "وضعیت با موفقیت به‌روزرسانی شد." });
        }


        public class AssignDto
        {
            public int UserId { get; set; }
        }

        public class StatusDto
        {
            public string Status { get; set; } = string.Empty;
        }

        [HttpGet("admin/charts")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetChartData()
        {
            // گرفتن لیست کاربران IT
            var itUsers = await _context.Users
                .Where(u => u.Role == "IT")
                .ToListAsync();

            // گرفتن تمام تیکت‌هایی که به کارشناسان IT ارجاع داده شده‌اند
            var tickets = await _context.Tickets
                .Include(t => t.AssignedToUser)
                .Where(t => t.AssignedToUserId != null)
                .ToListAsync();

            // گروه‌بندی تیکت‌ها بر اساس کارشناس برای نمودار دونات
            var donutData = tickets
                .GroupBy(t => t.AssignedToUser!.FullName)
                .Select(g => new
                {
                    user = g.Key,
                    count = g.Count()
                })
                .ToList();

            // داده‌ها برای نمودار bar: هر وضعیت برای هر کارشناس
            var barData = itUsers.Select(user =>
            {
                var userTickets = tickets.Where(t => t.AssignedToUserId == user.Id);
                return new
                {
                    user = user.FullName,
                    pending = userTickets.Count(t => t.Status == null || t.Status == "در انتظار بررسی"),
                    inprogress = userTickets.Count(t => t.Status == "در حال انجام"),
                    done = userTickets.Count(t => t.Status == "انجام شده"),
                    canceled = userTickets.Count(t => t.Status == "باطل شده")
                };
            }).ToList();

            return Ok(new
            {
                donut = donutData,
                bar = barData
            });
        }



        // 4. API: Get Notifications for Logged-in User
        [HttpGet("/api/notifications")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);

            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Take(10)
                .ToListAsync();

            return Ok(notifications);
        }

    }
}
