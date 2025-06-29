using API.Features.Logs.Common;

namespace API.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<(List<LogEntryDto> Logs, int TotalCount)> GetLogsAsync(int page, int size);
    }
}
