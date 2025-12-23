using Microsoft.AspNetCore.Mvc;
using PaymentsService.Data;
using PaymentsService.Models;

namespace PaymentsService.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PaymentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("create-account")]
    public IActionResult CreateAccount(int userId)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.UserId == userId);
        if (account != null)
            return Conflict("Account already exists.");

        _context.Accounts.Add(new Account { UserId = userId, Balance = 0 });
        _context.SaveChanges();
        return Ok("Account created.");
    }

    [HttpPost("add-funds")]
    public IActionResult AddFunds(int userId, decimal amount)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.UserId == userId);
        if (account == null)
            return NotFound("Account not found.");

        account.Balance += amount;
        _context.SaveChanges();
        return Ok($"Added {amount} to account. New balance: {account.Balance}");
    }

    [HttpGet("balance")]
    public IActionResult GetBalance(int userId)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.UserId == userId);
        if (account == null)
            return NotFound("Account not found.");

        return Ok(account.Balance);
    }
}