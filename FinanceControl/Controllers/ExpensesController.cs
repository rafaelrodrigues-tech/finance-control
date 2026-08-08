using FinanceControl.Data;
using FinanceControl.Models;
using FinanceControl.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
    public async Task<IActionResult> Create([FromBody]Expense expense)
    {
        
        await _expenseService.CreateExpense(expense);
        return Ok();// 200
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var expenses = await _expenseService.GetAllExpenses();
        return Ok(expenses);
    }
}
