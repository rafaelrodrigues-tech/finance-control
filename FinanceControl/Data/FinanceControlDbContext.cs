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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Expense>(b =>
        {
            // Garante que a chave primária é gerada automaticamente pelo banco
            b.HasKey(e => e.Id);
            b.Property(e => e.Id).ValueGeneratedOnAdd();

            // Sua configuração existente da data
            b.Property(e => e.DueDate).HasColumnType("date");
        });
    }
    public DbSet<Expense> Expenses { get; set; }

}
