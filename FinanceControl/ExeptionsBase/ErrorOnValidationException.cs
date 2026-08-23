namespace FinanceControl.ExeptionsBase;
public class ErrorOnValidationException :FinanceControlException
{//Quem transforma o erro em exceção
    private readonly List<string> _errors;
    public ErrorOnValidationException(List<string> errorMessage)
    {
        _errors = errorMessage;//Guardar a lista na exceção
    }
    public List<string> GetErrorMessages() => _errors;//Obter a lista depois

}
