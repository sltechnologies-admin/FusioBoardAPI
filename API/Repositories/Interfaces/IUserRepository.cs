using API.Models.Requests;

namespace API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email, string username);
        Task InsertUserAsync(RegisterRequest request);
    }
}
