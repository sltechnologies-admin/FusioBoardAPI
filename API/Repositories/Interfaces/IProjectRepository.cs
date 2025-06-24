using API.Common.Models;
using API.Features.Projects.Entities;
using API.Features.Users.Common;
using API.Features.Users.Entities;

namespace API.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<ProjectEntity?> GetByIdAsync(int id);
        Task<List<ProjectEntity>> GetAllAsync();
        Task<Result<bool>> UpdateAsync(ProjectEntity entity);

    }
}
