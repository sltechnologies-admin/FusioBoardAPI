using Microsoft.Extensions.Logging;

namespace API.Common.Logging
{
    /// <summary>
    /// Centralized helper for logging to SQL and optionally to ILogger.
    /// </summary>
    public static class LogHelper
    {
        public static async Task LogErrorAsync(
            ISqlLogger sqlLogger,
            string eventCode,
            string correlationId,
            string userMessage,
            string? exceptionDetails = null,
            string? module = null,
            string? layer = null,
            string? method = null,
            int? userId = null,
            string? requestedBy = null,
            string? source = null,
            object? inputParams = null,
            string? requestUrl = null,
            string? httpMethod = null,
            string? clientIp = null)
            {
                if (sqlLogger == null) throw new ArgumentNullException(nameof(sqlLogger));

                await sqlLogger.LogAsync(new LogEntry {
                    LogLevel = "Error",
                    EventCode = eventCode,
                    CorrelationId = correlationId,
                    UserMessage = userMessage,
                    TechnicalDetails = exceptionDetails ?? string.Empty,
                    Module = module,
                    Layer = layer,
                    Method = method,
                    UserId = userId,
                    RequestedBy = requestedBy,
                    Source = source,
                    InputParameters = inputParams,
                    RequestUrl = requestUrl,
                    HttpMethod = httpMethod,
                    ClientIP = clientIp
                });
            }

        /// <summary>
        /// Logs a warning-level message.
        /// </summary>
        public static async Task LogWarningAsync(
            ISqlLogger sqlLogger,
            string eventCode,
            string correlationId,
            string warningMessage)
        {
            if (sqlLogger == null) throw new ArgumentNullException(nameof(sqlLogger));

            await sqlLogger.LogAsync(new LogEntry {
                LogLevel = "Warning",
                EventCode = eventCode,
                CorrelationId = correlationId,
                UserMessage = warningMessage,
                TechnicalDetails = null
            });
        }

        /// <summary>
        /// Logs an info-level message.
        /// </summary>
        public static async Task LogInfoAsync(
            ISqlLogger sqlLogger,
            string eventCode,
            string correlationId,
            string infoMessage)
        {
            if (sqlLogger == null) throw new ArgumentNullException(nameof(sqlLogger));

            await sqlLogger.LogAsync(new LogEntry {
                LogLevel = "Information",
                EventCode = eventCode,
                CorrelationId = correlationId,
                UserMessage = infoMessage,
                TechnicalDetails = null
            });
        }

        #region Future Enhancements (Suggestions)
        // - Add overloads with optional payload or request metadata
        // - Add ability to enrich logs with userId or tenantId from context
        // - Support structured logging into Serilog or other sinks
        #endregion
    }
}
