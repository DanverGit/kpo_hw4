namespace OrdersService.Models;

public class OutboxMessage
{
    public int Id { get; set; }
    public string EventName { get; set; }
    public string Payload { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Processed { get; set; }
}