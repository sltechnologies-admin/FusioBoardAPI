using API.Common.Models;
using API.Features.Logs.Common;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<Result<List<LogEntryDto>>> GetLogsAsync(int page, int size)
        {
            try
            {
                var logs = await _logRepository.GetLogsAsync(page, size);
                return Result<List<LogEntryDto>>.SuccessResult(logs);
            }
            catch (Exception ex)
            {
                return Result<List<LogEntryDto>>.Fail(
                    Messages.Log.e_UnexpectedErrorFetchingLogs,
                    ex.ToString()
                );
            }
        }

    }
}
