using API.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging; // <-- Make sure this is included
using System.Data;
using System.Text.Json;

namespace API.Common.Logging
{
    public class SqlLogger : ISqlLogger
    {
        private readonly IDatabaseService _db;
        private readonly ILogger<SqlLogger> _logger; // <-- Add this

        public SqlLogger(IDatabaseService db, ILogger<SqlLogger> logger) // <-- Inject it
        {
            _db = db;
            _logger = logger;
        }

        public async Task LogAsync(LogEntry entry)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@LogLevel", entry.LogLevel ?? (object)DBNull.Value),
                    new SqlParameter("@EventCode", entry.EventCode ?? (object)DBNull.Value),
                    new SqlParameter("@CorrelationId", entry.CorrelationId ?? (object)DBNull.Value),
                    new SqlParameter("@UserMessage", entry.UserMessage ?? (object)DBNull.Value),
                    new SqlParameter("@TechnicalDetails", entry.TechnicalDetails ?? (object)DBNull.Value),
                    new SqlParameter("@Module", entry.Module ?? (object)DBNull.Value),
                    new SqlParameter("@Layer", entry.Layer ?? (object)DBNull.Value),
                    new SqlParameter("@Method", entry.Method ?? (object)DBNull.Value),
                    new SqlParameter("@RequestedBy", entry.RequestedBy ?? (object)DBNull.Value),
                    new SqlParameter("@Source", entry.Source ?? (object)DBNull.Value),
                    new SqlParameter("@UserId", entry.UserId ?? (object)DBNull.Value),
                    new SqlParameter("@InputParameters", entry.InputParameters == null
                        ? (object)DBNull.Value
                        : JsonSerializer.Serialize(entry.InputParameters)),
                    new SqlParameter("@RequestUrl", entry.RequestUrl ?? (object)DBNull.Value),
                    new SqlParameter("@HttpMethod", entry.HttpMethod ?? (object)DBNull.Value),
                    new SqlParameter("@ClientIP", entry.ClientIP ?? (object)DBNull.Value),
                };

                await _db.ExecuteNonQueryAsync("sp_fb_InsertLog", parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"[LoggingFailure] Could not write to Logs table: {ex.Message}");
#else
                _logger?.LogError(ex, "Failed to write log entry to SQL");
#endif
            }
        }
    }
}
