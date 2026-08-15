using FinanceControl.Models;
using FluentValidation;
namespace FinanceControl.Validators;

public class ExpenseValidator : AbstractValidator<Expense>
{
    public ExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage("O nome não pode ser vazio")
            .Length(3,30).WithMessage("O nome deve ter entre 3 e 30 caracteres. ");

        RuleFor(expense => expense.Amount).Must(Amount => Amount > 0).WithMessage("O valor deve ser maior que Zero");
        RuleFor(expense => expense.IsPaid).NotEmpty().WithMessage("Se for pago, deve ser true. Se não for pago, deve ser false");

    }
}