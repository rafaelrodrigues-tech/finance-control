using FinanceControl.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Data;

public class FinanceControlDbContext : DbContext
{
    public FinanceControlDbContext(DbContextOptions<FinanceControlDbContext> options) 
        : base(options)
    {
    }
    public DbSet<Expense> Expenses { get; set; }
}
