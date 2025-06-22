using API.Features.Users.Entities;
using API.Models.Requests;

namespace API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user entity by its primary key.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// A Task containing the UserEntity if found; otherwise, null.
        /// </returns>
        Task<UserEntity?> GetByIdAsync(int userId);

        Task<IReadOnlyList<UserEntity>> GetAllAsync();

        Task<bool> UserExistsAsync(string email, string username);
        Task UpsertUserAsync(RegisterRequest request);
    }
}
