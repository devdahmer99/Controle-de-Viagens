using Journey.Communication.Responses;
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

                var responseJson = new ResponseErrorsJson(journeException.GetErrorMessages());

                context.HttpContext.Response.StatusCode = (int)journeException.GetStatusCode();

                context.Result = new NotFoundObjectResult(responseJson);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var error = new List<string>
                {
                    "Erro desconhecido"
                };

                var responseJson = new ResponseErrorsJson(error);
                
                context.Result = new ObjectResult(responseJson);
            }
        }
    }
}
