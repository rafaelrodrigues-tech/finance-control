using System.ComponentModel;

namespace FinanceControl.Models;
public class Expense
{
    public int Id { get; private set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool IsPaid { get; set; } = false;
    public string? Description { get; set; } = string.Empty;

}
