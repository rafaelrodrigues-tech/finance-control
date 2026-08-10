using FinanceControl.Data;
using FinanceControl.Models;
using FinanceControl.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using 

namespace FinanceControl.Controllers;

[Route("[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{

    private readonly ExpenseService _expenseService;
    public ExpensesController(ExpenseService expenseService)
    {
        _expenseService = expenseService;

    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
    {

        await _expenseService.AddExpense(expense);
        return Ok();// 200
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExpenses()
    {
        var expenses = await _expenseService.ListOfExpenses();
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpenseById(int id)
    {
        var idExpense = await _expenseService.FindByExpense(id);
        if (idExpense is null)
        {
            return NotFound("Despesa não encontrada");
        }

        return Ok(idExpense);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id, Expense expense)
    {
        var idExpense = await _expenseService.FindByExpense(id);
        if (idExpense is null)
        {
            return NotFound("Despesa não encontrada");
        }
        await _expenseService.RemoveExpense(idExpense);
        return Ok("Despesa removida com sucesso");
    }
}
