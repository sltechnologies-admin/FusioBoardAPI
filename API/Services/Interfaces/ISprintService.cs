
using API.Common.Models;
using API.Features.Sprints.Common;
using API.Features.Sprints.Common.API.Features.Sprints.Common;

namespace API.Services.Interfaces
{
    public interface ISprintService
    {
        Task<Result<List<SprintResponseDto>>> GetAllByProjectIdAsync(int projectId);
        Task<Result<SprintDto>> GetByIdAsync(int id);
        Task<Result<int>> CreateAsync(SprintCreateDto dto, int userId);
        Task<Result<int>> UpdateAsync(SprintUpdateDto dto, int userId);
        Task<Result<bool>> DeleteAsync(int id, int userId);
    }
}
