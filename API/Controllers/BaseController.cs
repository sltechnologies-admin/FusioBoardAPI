using API.Common.Logging;
using API.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected string CorrelationId =>
            HttpContext?.Items[HeaderKeys.CorrelationId]?.ToString()
            ?? HttpContext?.Request.Headers[HeaderKeys.CorrelationId].FirstOrDefault()
            ?? Guid.NewGuid().ToString();

        protected ISqlLogger SqlLogger =>
            HttpContext.RequestServices.GetRequiredService<ISqlLogger>();

        protected async Task<IActionResult> HandleFailureAsync(
            string eventCode,
            string userMessage,
            string internalMessage,
            bool isException = false,
            bool logToConsole = false)
        {
            if (logToConsole)
            {
                var logger = HttpContext.RequestServices.GetRequiredService<ILogger<BaseController>>();
                logger.LogError("[{EventCode}] CorrelationId: {CorrelationId} - {UserMessage} | {Details}",
                    eventCode, CorrelationId, userMessage, internalMessage);
            }

            await LogHelper.LogErrorAsync(SqlLogger, eventCode, CorrelationId,userMessage, internalMessage);
                return StatusCode(HttpStatusCodes.InternalServerError, new {
                    message = "An unexpected error occurred.",
                    correlationId = CorrelationId,
                    eventCode = eventCode,
                    userMessage = userMessage,
                    internalMessage = internalMessage

                });
        }
    }
}
