using API.Models.Requests;
using Azure.Core;

namespace API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string ErrorMessage)> RegisterUserAsync(RegisterRequest request);
    }

}
