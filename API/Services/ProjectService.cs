using API.Common.Models;
using API.Features.Projects.Common;
using API.Features.Projects.Entities;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;

        public ProjectService(IProjectRepository projectRepository)
        {
            _repo = projectRepository;
        }

        public async Task<Result<int>> CreateAsync(CreateProjectRequest request)
        {
            return await _repo.CreateAsync(request);
        }

        public async Task<Result<ProjectDto>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                if (entity == null)
                    return Result<ProjectDto>.Fail($"Project with ID {id} not found.");

                var resultData = new ProjectDto {
                    ProjectId = entity.ProjectId,
                    Name = entity.Name,
                    Description = entity.Description,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    CreatedBy = entity.CreatedBy,
                    IsActive = entity.IsActive
                   
                };

                return Result<ProjectDto>.SuccessResult(resultData);
            }
            catch (Exception ex)
            {
                return Result<ProjectDto>.Fail("An error occurred while retrieving the project.");
            }
        }


        public async Task<Result<List<ProjectDto>>> GetAllAsync()
        {
            try
            {
                var entities = await _repo.GetAllAsync();

                var resultData = entities.Select(e => new ProjectDto {
                    ProjectId = e.ProjectId,
                    Name = e.Name,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    CreatedBy = e.CreatedBy,
                    IsActive = e.IsActive,
                }).ToList();

                return Result<List<ProjectDto>>.SuccessResult(resultData);
            }
            catch (Exception ex)
            {
                return Result<List<ProjectDto>>.Fail(
                    "An error occurred while fetching projects.",
                    ex.ToString());  // Keep full trace for logging
            }


        }
        public async Task<Result<bool>> UpdateAsync(UpdateProjectRequest request)
        {
            var entity = new UpdateProjectRequest {
                ProjectId = request.ProjectId,
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsActive = request.IsActive
            };

            return await _repo.UpdateAsync(entity);
        }

    }

}
