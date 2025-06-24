using API.Common.Models;
using API.Constants;
using API.Features.Users.Common;
using API.Models.Requests;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository AuthRepository)
        {
            _repo = AuthRepository;
        }

        public async Task<Result<UserDto>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                if (entity == null)
                    return Result<UserDto>.Fail($"User with ID {id} not found.");

                var resultData = new UserDto {
                    UserId = entity.UserId,
                    Username = entity.Username,
                    Email = entity.Email,
                    IsActive = entity.IsActive,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                };

                return Result<UserDto>.SuccessResult(resultData);
            }
            catch (Exception ex)
            {
                return Result<UserDto>.Fail("An error occurred while retrieving the user.");
            }
        }


        public async Task<Result<List<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var entities = await _repo.GetAllAsync();

                var resultData = entities
                    .Select(e => new UserDto {
                        UserId = e.UserId,
                        Username = e.Username,
                        Email = e.Email,
                        IsActive = e.IsActive,
                        CreatedAt = e.CreatedAt,
                        UpdatedAt = e.UpdatedAt
                    })
                    .ToList();

                return Result<List<UserDto>>.SuccessResult(resultData);
            }
            catch (Exception ex)
            {
                return Result<List<UserDto>>.Fail("Failed to retrieve users.");
            }
        }



        public async Task<(bool Success, string ErrorMessage)> UpsertUserAsync(RegisterRequest request)
        {
            var exists = await _repo.UserExistsAsync(request.Email, request.Username);
            if (exists)
                return (false, Messages.User.i_UserAlreadyExists);

            await _repo.UpsertUserAsync(request);
            return (true, null);
        }

        public async Task<Result<List<UserRoleDto>>> GetUserRolesAsync(int userId)
        {
            var resultData = await _repo.GetUserRolesAsync(userId);
            return Result<List<UserRoleDto>>.SuccessResult(resultData);
        }


        #region practice
        /* Repository returns an entity
        var entity = await _userRepository.GetByIdAsync(id); // returns UserEntity

        // Service maps to DTO
        var dto = _mapper.Map<UserDto>(entity);

        //Controller returns 200 OK with UserDto
        return Ok(dto);
        */
        #endregion
    }

}


