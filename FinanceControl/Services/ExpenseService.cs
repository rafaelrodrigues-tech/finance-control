using FinanceControl.Data;
using FinanceControl.Dtos;
using FinanceControl.Models;
using Microsoft.EntityFrameworkCore;

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


    public async Task AddExpense(Expense expense)// Criar uma despesa POST
    {
        await _financeControlDbContext.Expenses.AddAsync(expense);
        await _financeControlDbContext.SaveChangesAsync();
    }
    public async Task<List<Expense>> ListOfExpenses()// listar todas as despesas, organizadas em ordem de data de vencimento.
    {
        return await _financeControlDbContext.Expenses
               .OrderBy(x => x.DueDate)
               .ToListAsync();
    }
    public async Task<Expense?> FindByExpense(int id)// Busca de despesa pelo ID
    {
        return await _financeControlDbContext.Expenses
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task RemoveExpense(Expense expense)// remove uma despesa 
    {
        _financeControlDbContext.Expenses.Remove(expense);
        await _financeControlDbContext.SaveChangesAsync();

    }
    public async Task UpdateExpenseDTO(Expense idExpense, UpdateExpenseDto Dto)
    {
        //Mapeamento na unha =>  
        if (Dto.Title != null) idExpense.Title = Dto.Title;
        if (Dto.Amount != null) idExpense.Amount = Dto.Amount.Value;
        if (Dto.DueDate != null) idExpense.DueDate = Dto.DueDate.Value;
        if (Dto.IsPaid != null) idExpense.IsPaid = Dto.IsPaid.Value;
        if (Dto.Description != null) idExpense.Description = Dto.Description;


        await _financeControlDbContext.SaveChangesAsync();
    }

}
