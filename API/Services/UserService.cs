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

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            var entity = await _repo.GetByIdAsync(userId);
            if (entity == null) return null;
            return new UserDto {
                UserId = entity.UserId,
                Username = entity.Username,
                Email = entity.Email,
                IsActive = entity.IsActive
            };
        }

        public async Task<IReadOnlyList<UserDto>> GetAllUsersAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities
                .Select(e => new UserDto {
                    UserId = e.UserId,
                    Username = e.Username,
                    Email = e.Email,
                    IsActive = e.IsActive,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                })
                .ToList();
        }

        public async Task<(bool Success, string ErrorMessage)> UpsertUserAsync(RegisterRequest request)
        {
            var exists = await _repo.UserExistsAsync(request.Email, request.Username);
            if (exists)
                return (false, Messages.User.i_UserAlreadyExists);

            await _repo.UpsertUserAsync(request);
            return (true, null);
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


