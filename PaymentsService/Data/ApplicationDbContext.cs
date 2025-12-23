using Microsoft.EntityFrameworkCore;
using PaymentsService.Models;
using System.Collections.Generic;

namespace PaymentsService.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}