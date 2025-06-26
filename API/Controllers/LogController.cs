using Microsoft.AspNetCore.Mvc;
using API.Common.Logging;
using API.Services.Interfaces;
using API.Constants;

namespace API.Controllers
{
    public class LogController : BaseController
    {
        private readonly ILogService _service;
        private readonly ISqlLogger _sqlLogger;
        private readonly IAppLogger<SprintController> _logger;

        public LogController(ILogService service, ISqlLogger sqlLogger, IAppLogger<SprintController> logger)
        {
            _service = service;
            _sqlLogger = sqlLogger;
            _logger = logger;
        }


        /// <summary>
        /// Fetches system logs with pagination support for admin monitoring.
        /// </summary>
        /// <param name="page">The page number (default is 1).</param>
        /// <param name="size">The number of records per page (default is 20).</param>
        /// <returns>A paginated list of logs or an appropriate error response.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetLogs([FromQuery] int page = 1, [FromQuery] int size = 20)
        {
            const string eventCode = EventCodes.Log.FetchAllError;
            const string userMessage = Messages.Log.e_UnexpectedErrorFetchingLogs;

            try
            {
                var result = await _service.GetLogsAsync(page, size);

                if (!result.Success)
                {
                    return await HandleFailureAsync(
                        eventCode,
                        result.ErrorMessage,
                        result.TechnicalDetails
                    );
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return await HandleFailureAsync(
                    eventCode,
                    userMessage,
                    ex.ToString(),
                    logToConsole: true
                );
            }
        }



    }
}
