using Microsoft.AspNetCore.Mvc;

namespace SupportTicketSystem.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        [HttpGet("admin")]
        public IActionResult GetAdminData()
        {
            var cards = new[]
            {
                new { label = "تیکت‌های جدید", count = 12 },
                new { label = "در حال بررسی", count = 5 },
                new { label = "پاسخ داده شده", count = 22 },
                new { label = "بسته شده", count = 7 },
            };

            var tickets = new[]
            {
                new { title = "مشکل لاگین", status = "باز", assignee = "واحد IT" },
                new { title = "درخواست افزایش دسترسی", status = "در حال بررسی", assignee = "مدیر" },
                new { title = "بروزرسانی نرم‌افزار", status = "انجام شده", assignee = "پشتیبانی" },
            };

            return Ok(new { cards, tickets });
        }
    }
}
