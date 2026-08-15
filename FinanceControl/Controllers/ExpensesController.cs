using FinanceControl.Dtos;
using FinanceControl.ExeptionsBase;
using FinanceControl.Models;
using FinanceControl.Services;
using FinanceControl.Validators;
using Microsoft.AspNetCore.Mvc;

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
        
        await ValidateAndThrowOnFailures(expense);// primeira validação

        await _expenseService.AddExpense(expense);
        return Created();
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
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var idExpense = await _expenseService.FindByExpense(id);
        if (idExpense is null)
        {
            return NotFound("Despesa não encontrada");
        }
        await _expenseService.RemoveExpense(idExpense);
        return Ok("Despesa removida com sucesso");
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateExpense([FromRoute]int id,[FromBody]UpdateExpenseDto Dto)
    {
        var idExpense = await _expenseService.FindByExpense(id);

        if (idExpense is null)
        {
            return NotFound("Despesa não encontrada");
        }

        //passa a despesa junto com a alteração(DTO) para o service
        await _expenseService.UpdateExpenseDTO(idExpense, Dto);
        return Ok();
    }
    private async Task ValidateAndThrowOnFailures(Expense expense)
    {
        var validator = new ExpenseValidator();
        var result = validator.Validate(expense);

        if (result.IsValid == false)
        {
            var ErrorMessages = result.Errors.Select(erros => erros.ErrorMessage).ToList();
            throw new ErrorOnValidationException(ErrorMessages);
        }
    }
}
