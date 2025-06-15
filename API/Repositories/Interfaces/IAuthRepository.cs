using API.Models.Requests;

namespace API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> UserExistsAsync(string email, string username);
        Task InsertUserAsync(RegisterRequest request);
    }
}
