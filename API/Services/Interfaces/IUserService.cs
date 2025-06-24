using API.Common.Models;
using API.Features.Users.Common;
using API.Models.Requests;
using Azure.Core;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">Primary key of the user.</param>
        /// <returns>User DTO if found; otherwise, null.</returns>
        Task<Result<UserDto>> GetByIdAsync(int id);

        Task<Result<List<UserDto>>> GetAllUsersAsync();
        Task<Result<List<UserRoleDto>>> GetUserRolesAsync(int id);


        Task<(bool Success, string ErrorMessage)> UpsertUserAsync(RegisterRequest request);
    }

}
