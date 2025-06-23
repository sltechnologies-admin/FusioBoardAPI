using API.Features.Projects.Common;

namespace API.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto?> GetProjectByIdAsync(int id);
    }
}
