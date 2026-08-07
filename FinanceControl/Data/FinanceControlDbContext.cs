using FinanceControl.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Data;

public class FinanceControlDbContext : DbContext
{
    public FinanceControlDbContext(DbContextOptions<FinanceControlDbContext> options) 
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>()
            .Property(e => e.DueDate)
            .HasColumnType("date");
    }
    public DbSet<Expense> Expenses { get; set; }

}
