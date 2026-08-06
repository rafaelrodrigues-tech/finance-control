
namespace FinanceControl.Models;


public class Expense
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; } 
    public string Description { get; set; } = string.Empty;
}
