using API.Features.Logs.Common;

namespace API.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<List<LogEntryDto>> GetLogsAsync(int pageNumber, int pageSize);
    }
}
