using API.Features.Projects.Entities;
using API.Features.Users.Entities;

namespace API.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<ProjectEntity?> GetByIdAsync(int userId);
    }
}
