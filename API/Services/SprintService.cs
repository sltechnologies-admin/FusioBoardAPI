using API.Common.Models;
using API.Features.Sprints.Common;
using API.Features.Sprints.Common.API.Features.Sprints.Common;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _sprintRepository;

        public SprintService(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }

        public async Task<Result<List<SprintResponseDto>>> GetAllByProjectIdAsync(int projectId)
        {
            var result = await _sprintRepository.GetAllByProjectIdAsync(projectId);

            if (!result.IsSccess)
                return Result<List<SprintResponseDto>>.Fail(result.UserErrorMessage, result.TechnicalErrorDetails);

            var mapped = result.Data.Select(s => new SprintResponseDto {
                Id = s.Id,
                ProjectId = s.ProjectId,
                Name = s.Name,
                Goal = s.Goal,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IsActive = s.IsActive
            }).ToList();

            return Result<List<SprintResponseDto>>.SuccessResult(mapped);
        }

        public async Task<Result<SprintResponseDto>> GetByIdAsync(int id)
        {
            var result = await _sprintRepository.GetByIdAsync(id);

            if (!result.IsSccess || result.Data == null)
                return Result<SprintResponseDto>.Fail(result.UserErrorMessage ?? "e_sprint_not_found", result.TechnicalErrorDetails ?? "Sprint not found.");

            var sprint = result.Data;

            var dto = new SprintResponseDto {
                Id = sprint.Id,
                ProjectId = sprint.ProjectId,
                Name = sprint.Name,
                Goal = sprint.Goal,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                IsActive = sprint.IsActive
            };

            return Result<SprintResponseDto>.SuccessResult(dto);
        }

        public Task<Result<int>> CreateAsync(SprintCreateDto dto, int userId)
        {
            return _sprintRepository.CreateAsync(dto, userId);
        }

        public Task<Result<int>> UpdateAsync(SprintUpdateDto dto, int userId)
        {
            return _sprintRepository.UpdateAsync(dto, userId);
        }

        public Task<Result<bool>> DeleteAsync(int id, int userId)
        {
            return _sprintRepository.DeleteAsync(id, userId);
        }
    }
}
