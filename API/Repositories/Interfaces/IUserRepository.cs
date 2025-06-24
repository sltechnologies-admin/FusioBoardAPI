using API.Features.Users.Common;
using API.Features.Users.Entities;
using API.Models.Requests;

namespace API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user entity by its primary key.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>
        /// A Task containing the UserEntity if found; otherwise, null.
        /// </returns>
        Task<UserEntity?> GetByIdAsync(int id);
        Task<IReadOnlyList<UserDto>> GetAllAsync();
        Task<List<UserRoleDto>> GetUserRolesAsync(int id);
        Task<bool> UserExistsAsync(string email, string username);
        Task UpsertUserAsync(RegisterRequest request);
    }
}
