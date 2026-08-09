using FinanceControl.Data;
using FinanceControl.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinanceControl.Services;

public class ExpenseService
{
    private readonly FinanceControlDbContext _financeControlDbContext;


    public ExpenseService(FinanceControlDbContext financeControlDbContext)
    {
        _financeControlDbContext = financeControlDbContext;
    }
    //Tasks
    //UpdateExpense()
    //DeleteExpense()

    public async Task CreateExpense(Expense expense)// Criar uma despesa POST
    {
        await _financeControlDbContext.Expenses.AddAsync(expense);
        await _financeControlDbContext.SaveChangesAsync();
    }
    public async Task<List<Expense>> GetAllExpenses()// listar todas as despesas, organizadas em ordem de data de vencimento.
    {
        return await _financeControlDbContext.Expenses
               .OrderBy(x => x.DueDate)
               .ToListAsync();
    }
    public async Task<Expense?> GetExpenseById(int id)
    {
        return await _financeControlDbContext.Expenses
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
