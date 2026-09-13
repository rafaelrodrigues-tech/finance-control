using FinanceControl.Converters;
using FinanceControl.Data;
using FinanceControl.Dtos;
using FinanceControl.ExeptionsBase;
using FinanceControl.Models;
using FinanceControl.Validators;
using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Services;

public class ExpenseService
{
    private readonly FinanceControlDbContext _financeControlDbContext;

    public ExpenseService(FinanceControlDbContext financeControlDbContext)
    {
        _financeControlDbContext = financeControlDbContext;
    }
    public async Task AddExpense(Expense expense)// Criar uma despesa POST
    {
        expense.Title = expense.Title.NormalizeTitle();
        expense.Description = expense.Description.NormalizeDescription();

        ValidateAndThrowOnFailures(expense);// Validação da request

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
    public async Task UpdateExpenseDTO(Expense expense, UpdateExpenseDto Dto)
    {
        //Mapeamento na unha =>  
        if (Dto.Title != null) expense.Title = Dto.Title.NormalizeTitle();
        if (Dto.Amount != null) expense.Amount = Dto.Amount.Value;
        if (Dto.DueDate != null) expense.DueDate = Dto.DueDate.Value;
        if (Dto.IsPaid != null) expense.IsPaid = Dto.IsPaid.Value;
        if (Dto.Description != null) expense.Description = Dto.Description.NormalizeDescription();

        ValidateAndThrowOnFailures(expense);

        await _financeControlDbContext.SaveChangesAsync();
    }
    private static void ValidateAndThrowOnFailures(Expense expense)//Somente esse controller precisa ter o acesso da validação, por isso privado.
    {
        var validator = new ExpenseValidator();//atribui as regras
        var result = validator.Validate(expense); // executa as regras  => esse objeto passou em todas as regras?

        if (result.IsValid == false)//Se não passou,Se o resultado da validação NÃO for válido, entre aqui
        {
            var ErrorMessages = result.Errors.Select(erros => erros.ErrorMessage).ToList();//cria a lista
            throw new ErrorOnValidationException(ErrorMessages);
        }
    }
}
