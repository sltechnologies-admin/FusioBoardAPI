using API.Common.Models;
using API.Features.Sprints.Common;
using API.Features.Sprints.Common.API.Features.Sprints.Common;
using API.Features.Users.Common;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _repo;

        public SprintService(ISprintRepository sprintRepository)
        {
            _repo = sprintRepository;
        }

        public async Task<Result<List<SprintResponseDto>>> GetAllByProjectIdAsync(int projectId)
        {
            var result = await _repo.GetAllByProjectIdAsync(projectId);

            if (!result.IsSuccess)
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

        public async Task<Result<SprintDto>> GetByIdAsync(int id)
        {
            try
            {
                var reporesponse = await _repo.GetByIdAsync(id);
                if (reporesponse == null)
                    return Result<SprintDto>.Fail($"Sprint with ID: {id} not found.");

                return Result<SprintDto>.SuccessResult(reporesponse, reporesponse.TotalCount);
            }
            catch (Exception ex)
            {
                return Result<SprintDto>.Fail("An error occurred while retrieving the user.");
            }
        }

        public Task<Result<int>> CreateAsync(SprintCreateDto dto, int userId)
        {
            return _repo.CreateAsync(dto, userId);
        }

        public Task<Result<int>> UpdateAsync(SprintUpdateDto dto, int userId)
        {
            return _repo.UpdateAsync(dto, userId);
        }

        public Task<Result<bool>> DeleteAsync(int id, int userId)
        {
            return _repo.DeleteAsync(id, userId);
        }
    }
}
