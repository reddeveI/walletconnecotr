using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WalletConnector.Application.Common.Exceptions;

namespace WalletConnector.Api.Helpers
{
    public class ExceptionHandlerHelper
    {
        public ProblemDetails Do(HttpContext context, Exception exception, ILogger<ExceptionHandlerHelper> logger)
        {
            ProblemDetails details;

            switch (exception)
            {
                case ValidationException ve:
                    details = new ValidationProblemDetails(ve.Errors)
                    {
                        Detail = exception.ToString(),
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                    };
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotFoundException nfe:
                    details = new ProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        Title = "The specified resource was not found.",
                        Detail = nfe.Message
                    };
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnauthorizedAccessException uae:
                    details = new ProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Title = "Unauthorized",
                        Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                        Detail = uae.Message
                    };
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case WebException we:
                    details = new ProblemDetails()
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = we.Message
                    };
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    details = new ProblemDetails()
                    {
                        Detail = "Server error"
                    };
                    break;
            }
            
            logger.LogError(details.Detail);

            return details;
        }
    }
}