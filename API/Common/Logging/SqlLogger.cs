using API.Data.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace API.Common.Logging
{
    public class SqlLogger : ISqlLogger
    {
        private readonly IDatabaseService _db;

        public SqlLogger(IDatabaseService db)
        {
            _db = db;
        }

        //public async Task LogAsync(LogEntry entry)
        //{
        //    const string query = @"
        //INSERT INTO Logs (LogLevel, EventCode, CorrelationId, UserMessage, TechnicalDetails, CreatedAt)
        //VALUES (@LogLevel, @EventCode, @CorrelationId, @UserMessage, @TechnicalDetails, @CreatedAt)";

        //    var parameters = new List<SqlParameter>
        //    {
        //        new SqlParameter("@LogLevel", entry.LogLevel),
        //        new SqlParameter("@EventCode", entry.EventCode ?? (object)DBNull.Value),
        //        new SqlParameter("@CorrelationId", entry.CorrelationId ?? (object)DBNull.Value),
        //        new SqlParameter("@UserMessage", entry.UserMessage ?? (object)DBNull.Value),
        //        new SqlParameter("@TechnicalDetails", entry.TechnicalDetails ?? (object)DBNull.Value),
        //        //new SqlParameter("@CreatedAt", entry.CreatedAt)
        //    };

        //    await _db.ExecuteNonQueryAsync(query, parameters);
        //}

        #region  exmple: 
        /* 
                    🔒 Optional (Final Touches)
                    
                    If you want to fully finalize error logging:
                    ✅ 1. Catch block usage pattern (example)
                    
                    Use this consistently:
                    
                    try
                    {
                    // some business logic
                    }
                    catch (Exception ex)
                    {
                    await LogHelper.LogErrorAsync(
                       SqlLogger,
                       eventCode: EventCodes.e_Project_UpdateFailed,
                       correlationId: CorrelationId,
                       userMessage: "Something went wrong while updating the project.",
                       exceptionDetails: ex.ToString(),
                       module: LogModules.ProjectManagement,
                       layer: LogLayers.Controller,
                       method: nameof(UpdateProject),
                       requestedBy: "user:krish",
                       source: "ReactClient",
                       userId: currentUserId,
                       inputParams: new { id, dto },
                       requestUrl: HttpContext?.Request?.Path,
                       httpMethod: HttpContext?.Request?.Method,
                       clientIp: HttpContext?.Connection?.RemoteIpAddress?.ToString()
                    );
                    }         
        */
        #endregion
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
