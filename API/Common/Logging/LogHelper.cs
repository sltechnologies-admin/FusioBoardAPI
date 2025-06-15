
namespace API.Common.Logging
{
    public static class LogHelper
    {
        public static async Task LogErrorAsync(
            //    ILogger logger,
            ISqlLogger sqlLogger,
            string eventCode,
            string correlationId,
            string message,
            Exception ex)
        {
            // 1. Log to built-in ILogger
            //    logger.LogError(ex, "[{EventCode}] CorrelationId: {CorrelationId} - {Message}", eventCode, correlationId, message);

            // 2. Log to SQL log table
            await sqlLogger.LogAsync(new LogEntry {
                LogLevel = "Error",
                EventCode = eventCode,
                CorrelationId = correlationId,
                Message = message,
                Exception = ex.ToString()
            });
        }

        public static async Task LogWarningAsync(
            ILogger logger,
            ISqlLogger sqlLogger,
            string eventCode,
            string correlationId,
            string message)
        {
            //logger.LogWarning("[{EventCode}] CorrelationId: {CorrelationId} - {Message}", eventCode, correlationId, message);

            await sqlLogger.LogAsync(new LogEntry {
                LogLevel = "Warning",
                EventCode = eventCode,
                CorrelationId = correlationId,
                Message = message,
                Exception = null
            });
        }

        public static async Task LogInfoAsync(
            ILogger logger,
            ISqlLogger sqlLogger,
            string eventCode,
            string correlationId,
            string message)
        {
            //  logger.LogInformation("[{EventCode}] CorrelationId: {CorrelationId} - {Message}", eventCode, correlationId, message);

            await sqlLogger.LogAsync(new LogEntry {
                LogLevel = "Information",
                EventCode = eventCode,
                CorrelationId = correlationId,
                Message = message,
                Exception = null
            });
        }
    }
}
