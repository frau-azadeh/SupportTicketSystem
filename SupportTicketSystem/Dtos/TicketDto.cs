public class TicketDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public IFormFile? Attachment { get; set; }
}
