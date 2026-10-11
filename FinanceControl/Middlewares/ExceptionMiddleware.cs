using System.Text.Json;
using FinanceControl.ExeptionsBase;

namespace FinanceControl.Middlewares;

/// <summary>
/// Middleware global para captura e tratamento centralizado de exceções na aplicação.
/// Intercepta erros ocorridos durante a execução das requisições HTTP e devolve respostas formatadas em JSON.
/// </summary>
public class ExceptionMiddleware
{
    // Armazena a referência para o próximo middleware/etapa do pipeline do ASP.NET Core
    private readonly RequestDelegate _next;

    // Registrador de logs nativo do ASP.NET Core para gravar detalhes dos erros no console ou arquivos
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ErrorOnValidationException ex)// Erro esperado
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status400BadRequest,
                new { errors = ex.GetErrorMessages() }
            );
        }
        catch (Exception ex)//erro inesperado
        {
            // CAPTURA: Qualquer erro inesperado (falhas de banco de dados, NullReferenceException, bugs, etc.)

            // 1. Grava o erro completo (com StackTrace) nos logs internos do servidor para consulta dos desenvolvedores
            _logger.LogError(ex, "Exceção não tratada capturada pelo middleware.");

            // 2. RESPOSTA: HTTP 500 (Internal Server Error) enviando uma mensagem genérica por segurança
            await HandleExceptionAsync(
                context,
                StatusCodes.Status500InternalServerError,
                new { error = "Erro interno do servidor" }
            );
        }
    }

    /// <summary>
    /// Método auxiliar privado para formatar e enviar a resposta de erro em JSON ao cliente.
    /// </summary>
    private static async Task HandleExceptionAsync(HttpContext context, int statusCode, object responseBody)
    {
        // SEGURANÇA: Se a resposta já começou a ser transmitida ao cliente (ex: download parcial),
        // não podemos alterar o StatusCode nem os cabeçalhos HTTP, então encerramos para evitar novas exceções.
        if (context.Response.HasStarted)
        {
            return;
        }

        // Define o código de status HTTP correspondente (400, 500, etc.)
        context.Response.StatusCode = statusCode;

        // Define no cabeçalho HTTP que o conteúdo da resposta será formatado como JSON
        context.Response.ContentType = "application/json";

        // Configuração para formatar os nomes das propriedades JSON no padrão web camelCase (ex: "errors" em vez de "Errors")
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // Converte o objeto C# (anônimo) em uma string no formato JSON e escreve no fluxo de resposta da requisição
        await context.Response.WriteAsync(JsonSerializer.Serialize(responseBody, jsonOptions));
    }
}