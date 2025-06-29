using API.Features.Logs.Common;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Provides access to system logs with pagination support.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Retrieves paginated logs along with total count for admin monitoring.
        /// </summary>
        /// <param name="page">Page number (1-based).</param>
        /// <param name="size">Number of records per page.</param>
        /// <returns>A tuple containing the list of logs and total record count.</returns>
        Task<(List<LogEntryDto> Logs, int TotalCount)> GetLogsAsync(int page, int size);
    }
}
