using API.Common.Models;
using API.Features.Sprints.Common;
using API.Features.Sprints.Entities;

namespace API.Repositories.Interfaces
{
    public interface ISprintRepository
    {
        Task<Result<List<SprintEntity>>> GetAllByProjectIdAsync(int projectId);
        Task<Result<SprintEntity?>> GetByIdAsync(int id);
        Task<Result<int>> CreateAsync(SprintCreateDto dto, int userId);
        Task<Result<int>> UpdateAsync(SprintUpdateDto dto, int userId);
        Task<Result<bool>> DeleteAsync(int id, int userId);
    }
}
