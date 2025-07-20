using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Data;
using SupportTicketSystem.Models;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public TicketsController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitTicket([FromForm] TicketDto dto)
    {
        string filePath = null;

        if (dto.Attachment != null)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);
            var fileName = Guid.NewGuid() + Path.GetExtension(dto.Attachment.FileName);
            var path = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await dto.Attachment.CopyToAsync(stream);
            }

            filePath = "/uploads/" + fileName;
        }

        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            FilePath = filePath,
            CreatedBy = User.Identity?.Name ?? "کاربر ناشناس"
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return Ok(new { message = "تیکت با موفقیت ثبت شد." });
    }
}
