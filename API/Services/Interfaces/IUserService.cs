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
        /// <param name="userId">Primary key of the user.</param>
        /// <returns>User DTO if found; otherwise, null.</returns>
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task<IReadOnlyList<UserDto>> GetAllUsersAsync();

        Task<(bool Success, string ErrorMessage)> UpsertUserAsync(RegisterRequest request);
    }

}
