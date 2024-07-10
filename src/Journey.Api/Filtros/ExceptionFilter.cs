using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SQLitePCL;

namespace Journey.Api.Filtros
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JourneyException)
            {
                var journeException = (JourneyException)context.Exception;

                context.HttpContext.Response.StatusCode = (int)journeException.GetStatusCode();
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult("Erro Desconhecido");
            }
        }
    }
}
