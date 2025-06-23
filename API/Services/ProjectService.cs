using API.Features.Projects.Common;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;

        public ProjectService(IProjectRepository projectRepository)
        {
            _repo = projectRepository;
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return null;

            return new ProjectDto {
                ProjectId = entity.ProjectId,
                Name = entity.Name,
                Description = entity.Description,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                CreatedBy = entity.CreatedBy,
                IsActive = entity.IsActive
            };
        }
    }

}
