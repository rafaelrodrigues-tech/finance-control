using System.Text.Json;//converter objetos C# para JSON.
using FinanceControl.ExeptionsBase;

namespace FinanceControl.Middlewares;//Quem captura a exceção e transforma em resposta HTTP

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;//o próximo componente do pipeline(sequência de etapas ou instruções automatizadas
                                           //em que a saída de uma fase serve como entrada para a seguinte).

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)//HttpContext representa o contexto daquela requisição/resposta.
    {
        try
        {
            await _next(context);
        }
        catch (ErrorOnValidationException ex)// erros esperados(cliente)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var response = new//objeto c#
            {
                errors = ex.GetErrorMessages()//mensagens q foram guardadas
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response)
            );
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "Internal server error."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response)
            );
        }
    }
}
