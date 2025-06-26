using API.Common.Extensions;
using API.Data.Interfaces;
using API.Features.Logs.Common;
using API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace API.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IDatabaseService _db;

        public LogRepository(IDatabaseService db)
        {
            _db = db;
        }
        public async Task<List<LogEntryDto>> GetLogsAsync(int pageNumber, int pageSize)
        {
            var parameters = new[]
            {
               new SqlParameter("@PageNumber", pageNumber),
               new SqlParameter("@PageSize", pageSize)
            }.ToList(); //  Convert array to List<SqlParameter>

            return await _db.ExecuteReaderAsync("sp_fb_GetLogs", parameters, reader => new LogEntryDto {
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



    }
}
