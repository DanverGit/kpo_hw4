using Microsoft.AspNetCore.Mvc;
using OrdersService.Data;
using OrdersService.Models;

namespace OrdersService.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("create-order")]
    public IActionResult CreateOrder(int userId, decimal totalAmount)
    {
        var order = new Order
        {
            UserId = userId,
            TotalAmount = totalAmount,
            Status = "Created",
            OrderId = Guid.NewGuid().ToString()
        };

        _context.Orders.Add(order);
        _context.SaveChanges();

        return Ok($"Order created with ID: {order.OrderId}");
    }

    [HttpGet("list-orders")]
    public IActionResult ListOrders(int userId)
    {
        var orders = _context.Orders.Where(o => o.UserId == userId).ToList();
        return Ok(orders);
    }

    [HttpGet("order-status")]
    public IActionResult GetOrderStatus(string orderId)
    {
        var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null)
            return NotFound("Order not found.");

        return Ok(order.Status);
    }
}