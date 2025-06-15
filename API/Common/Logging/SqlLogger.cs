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
            var query = @"
                INSERT INTO Logs (LogLevel, EventCode, CorrelationId, Message, Exception, CreatedAt)
                VALUES (@LogLevel, @EventCode, @CorrelationId, @Message, @Exception, @CreatedAt)";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@LogLevel", entry.LogLevel),
                new SqlParameter("@EventCode", entry.EventCode ?? (object)DBNull.Value),
                new SqlParameter("@CorrelationId", entry.CorrelationId ?? (object)DBNull.Value),
                new SqlParameter("@Message", entry.Message ?? (object)DBNull.Value),
                new SqlParameter("@Exception", entry.Exception ?? (object)DBNull.Value),
                new SqlParameter("@CreatedAt", entry.CreatedAt)
            };

            await _db.ExecuteNonQueryAsync(query, parameters);
        }
    }
}
