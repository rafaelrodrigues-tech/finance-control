namespace FinanceControl.Dtos;

public class UpdateExpenseDto
{
    
    public string? Title { get; set; }
    public decimal? Amount { get; set; }
    public DateOnly? DueDate { get; set; } 
    public bool? IsPaid { get; set; } 
    public string? Description { get; set; }
}
