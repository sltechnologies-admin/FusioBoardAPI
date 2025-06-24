using API.Common.Models;
using API.Features.Projects.Common;
using API.Features.Projects.Entities;
using API.Features.Users.Common;

namespace API.Services.Interfaces
{
    public interface IProjectService
    {
        Task<Result<ProjectDto>> GetByIdAsync(int id);
        Task<Result<List<ProjectDto>>> GetAllAsync();
        Task<Result<bool>> UpdateAsync(UpdateProjectRequest request);

    }
}
