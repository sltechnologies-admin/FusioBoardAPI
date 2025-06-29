using API.Features.Logs.Common;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using System;

namespace API.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// Fetches paginated log entries and total count from repository.
        /// </summary>
        /// <param name="page">The page number (1-based).</param>
        /// <param name="size">Number of records per page.</param>
        /// <returns>Tuple: List of log entries and total count.</returns>
        public async Task<(List<LogEntryDto> Logs, int TotalCount)> GetLogsAsync(int page, int size)
        {
            try
            {
                return await _logRepository.GetLogsAsync(page, size);
            }
            catch (Exception)
            {
                // Optional: Add structured logging here
                return (new List<LogEntryDto>(), 0); // Safe fallback
            }
        }
    }
}
