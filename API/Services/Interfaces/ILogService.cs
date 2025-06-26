using API.Common.Models;
using API.Features.Logs.Common;
using System.Net.NetworkInformation;

namespace API.Services.Interfaces
{
        public interface ILogService
        {
        Task<Result<List<LogEntryDto>>> GetLogsAsync(int page, int size);
        }
}
