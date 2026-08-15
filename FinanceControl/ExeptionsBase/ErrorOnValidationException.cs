namespace FinanceControl.ExeptionsBase;

public class ErrorOnValidationException :FinanceControlException
{
    private readonly List<string> _errors;
    public ErrorOnValidationException(List<string> errorMessage)
    {
        _errors = errorMessage;
    }
    public List<string> GetErrorMessages() => _errors;

}
