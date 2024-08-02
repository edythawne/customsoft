using Domain.Response;
using Infrastructure.Exception;
using Newtonsoft.Json;

namespace Application.Middleware;

public class ExceptionMiddleware {

    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) {
        _next = next;
    }

    /**
     * Invoca el siguiente middleware
     * <param name="context"></param>
     */
    public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context) {
        try {
            await _next(context);
        } catch (System.Exception ex) {
            await HandleExceptionAsync(context, ex);
        }
    }

    /**
     * Manejo de la excepción y generación de la respuesta personalizada
     * <param name="context"></param>
     * <param name="exception"></param>
     */
    private async Task HandleExceptionAsync(Microsoft.AspNetCore.Http.HttpContext context, System.Exception exception) {
        Console.WriteLine(exception);
        // Genera la respuesta personalizada según el tipo de excepción
        var statusCode = StatusCodes.Status500InternalServerError;
        var message = exception.Message;

        if (exception is AuthenticationException a) {
            statusCode = a.Code;
        }
        
        if (exception is PermissionException p) {
            statusCode = p.Code;
        }
        
        if (exception is EntityException e) {
            statusCode = e.Code;
        }

        if (exception is NullResponseException n) {
            statusCode = n.Code;
        }

        // Configura la respuesta
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        var json = JsonConvert.SerializeObject(new ExceptionResponse(statusCode, message));

        // Escribe el mensaje de error en la respuesta
        await context.Response.WriteAsync(json);
    }
    
}