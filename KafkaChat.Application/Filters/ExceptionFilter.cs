using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KafkaChat.Application.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is Exception exception)
            {
                context.Result = new ObjectResult(new ApiResult
                {
                    Codigo = exception.HResult,
                    Mensagem = exception.Message
                })
                {
                    StatusCode = 500
                };
            }
        }
        private class ApiResult
        {
            public int Codigo { get; set; }
            public string Mensagem { get; set; }
        }
    }
}
