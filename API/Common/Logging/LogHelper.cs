using Microsoft.Extensions.Logging;

namespace API.Common.Logging
{
    /// <summary>
    /// Centralized helper for logging to SQL and optionally to ILogger.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Logs an error-level message to the SQL log store.
        /// </summary>
        public static async Task LogErrorAsync(
            ISqlLogger sqlLogger,
            string eventCode,
            string correlationId,
            string userMessage,
            string? technicalDetails = null)
        {
            if (sqlLogger == null) throw new ArgumentNullException(nameof(sqlLogger));

            var entry = new LogEntry {
                LogLevel = "Error",
                EventCode = eventCode,
                CorrelationId = correlationId,
                UserMessage = userMessage,
                TechnicalDetails = technicalDetails ?? string.Empty
            };

            await sqlLogger.LogAsync(entry);
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
