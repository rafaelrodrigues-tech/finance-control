namespace FinanceControl.Dtos;

public class UpdateExpenseDto
{
    
    public string? Title { get; set; } = string.Empty;
    public decimal? Amount { get; set; }
    public DateOnly? DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool? IsPaid { get; set; } = false;
    public string? Description { get; set; } = string.Empty;
}
