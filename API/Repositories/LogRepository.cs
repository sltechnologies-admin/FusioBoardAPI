using API.Common.Extensions;
using API.Data.Interfaces;
using API.Features.Logs.Common;
using API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IDatabaseService _db;

        public LogRepository(IDatabaseService db)
        {
            _db = db;
        }

        public async Task<(List<LogEntryDto> Logs, int TotalCount)> GetLogsAsync(int pageNumber, int pageSize)
        {
            var logs = new List<LogEntryDto>();
            int totalCount = 0;

            var parameters = new List<SqlParameter>
            {
                new("@PageNumber", pageNumber),
                new("@PageSize", pageSize)
            };

            await _db.ExecuteReaderMultiAsync(
                "sp_fb_GetLogs",
                parameters,
                async reader =>
                {
                    // Result set 1: logs
                    while (await reader.ReadAsync())
                    {
                        logs.Add(new LogEntryDto {
                            LogId = reader.GetInt32("LogId"),
                            LogLevel = reader.GetNullableString("LogLevel"),
                            EventCode = reader.GetNullableString("EventCode"),
                            CorrelationId = reader.GetNullableString("CorrelationId"),
                            UserMessage = reader.GetNullableString("UserMessage"),
                            TechnicalDetails = reader.GetNullableString("TechnicalDetails"),
                            Module = reader.GetNullableString("Module"),
                            Layer = reader.GetNullableString("Layer"),
                            Method = reader.GetNullableString("Method"),
                            RequestedBy = reader.GetNullableString("RequestedBy"),
                            Source = reader.GetNullableString("Source"),
                            UserId = reader.GetNullableInt("UserId"),
                            InputParameters = reader.GetNullableString("InputParameters"),
                            RequestUrl = reader.GetNullableString("RequestUrl"),
                            HttpMethod = reader.GetNullableString("HttpMethod"),
                            ClientIP = reader.GetNullableString("ClientIP"),
                            CreatedAt = reader.GetDateTimeOffset("CreatedAt")
                        });
                    }

                    // Move to second result set: TotalCount
                    if (await reader.NextResultAsync() && await reader.ReadAsync())
                    {
                        totalCount = reader.GetInt32("TotalCount");
                    }
                },
                CommandType.StoredProcedure
            );

            return (logs, totalCount);
        }
    }
}
