namespace OrdersService.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } // "Created", "Paid", "Failed"
    public string OrderId { get; set; } // Уникальный идентификатор заказа
}