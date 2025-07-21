using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportTicketSystem.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }

        public string Message { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; } = false;


        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
