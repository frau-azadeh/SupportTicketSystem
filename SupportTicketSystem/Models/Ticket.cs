using System.Globalization;

namespace SupportTicketSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "در انتظار بررسی";

        public string? Priority { get; set; }    
        public string? AttachmentPath { get; set; }  


        public int CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }

        public int? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }

        public string CreatedAtPersion
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                return $"{pc.GetYear(CreatedAt)}/{pc.GetMonth(CreatedAt):00}/{pc.GetDayOfMonth(CreatedAt):00}";
            }
        }

    }
}
