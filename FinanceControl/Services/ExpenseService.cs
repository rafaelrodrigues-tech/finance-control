using FinanceControl.Data;
using FinanceControl.Models;

namespace FinanceControl.Services;

public class ExpenseService
{
    private readonly FinanceControlDbContext _financeControlDbContext;

    public ExpenseService(FinanceControlDbContext financeControlDbContext)
    {
        _financeControlDbContext = financeControlDbContext;
    }
    //Tasks
    //GetAllExpenses()
    //GetExpenseById()
    //UpdateExpense()
    //DeleteExpense()

    public async Task CreateExpense(Expense expense)// Criar Conta POST
    {

        await _financeControlDbContext.Expenses.AddAsync(expense);
        await _financeControlDbContext.SaveChangesAsync();

    }


}
