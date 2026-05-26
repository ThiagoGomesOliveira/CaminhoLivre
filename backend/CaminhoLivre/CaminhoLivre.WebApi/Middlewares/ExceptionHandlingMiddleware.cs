using CaminhoLivre.Compartilhado.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CaminhoLivre.WebApi.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
{
    private readonly RequestDelegate _next = requestDelegate;

public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Segue o fluxo normal da requisição
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = StatusCodes.Status500InternalServerError;
        var title = "Erro interno no servidor";

        switch (exception)
        {
            case NotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                title = "Recurso não encontrado";
                break;

            case ConflictException:
                statusCode = StatusCodes.Status409Conflict;
                title = "Conflito de integridade";
                break;

            case BusinessRuleException:
                statusCode = StatusCodes.Status400BadRequest;
                title = "Violação de regra de negócio";
                break;
        }

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        var json = JsonSerializer.Serialize(problem);
        return context.Response.WriteAsync(json);
    }
}
