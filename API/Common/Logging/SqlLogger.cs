using API.Data.Interfaces;
using Microsoft.Data.SqlClient;

namespace API.Common.Logging
{
    public class SqlLogger : ISqlLogger
    {
        private readonly IDatabaseService _db;

        public SqlLogger(IDatabaseService db)
        {
            _db = db;
        }

        public async Task LogAsync(LogEntry entry)
        {
            const string query = @"
        INSERT INTO Logs (LogLevel, EventCode, CorrelationId, UserMessage, TechnicalDetails, CreatedAt)
        VALUES (@LogLevel, @EventCode, @CorrelationId, @UserMessage, @TechnicalDetails, @CreatedAt)";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@LogLevel", entry.LogLevel),
                new SqlParameter("@EventCode", entry.EventCode ?? (object)DBNull.Value),
                new SqlParameter("@CorrelationId", entry.CorrelationId ?? (object)DBNull.Value),
                new SqlParameter("@UserMessage", entry.UserMessage ?? (object)DBNull.Value),
                new SqlParameter("@TechnicalDetails", entry.TechnicalDetails ?? (object)DBNull.Value),
                new SqlParameter("@CreatedAt", entry.CreatedAt)
            };

            await _db.ExecuteNonQueryAsync(query, parameters);
        }

    }
}
