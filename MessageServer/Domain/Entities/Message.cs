namespace Domain.Entities;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int SerialNumber { get; set; }
    public DateTime Date { get; set; }
}
