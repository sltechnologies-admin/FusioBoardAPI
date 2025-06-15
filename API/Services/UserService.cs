using API.Constants;
using API.Models.Requests;
using API.Repositories.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _authRepository;

        public UserService(IUserRepository AuthRepository)
        {
            _authRepository = AuthRepository;
        }

        public async Task<(bool Success, string ErrorMessage)> RegisterUserAsync(RegisterRequest request)
        {
            var exists = await _authRepository.UserExistsAsync(request.Email, request.Username);
            if (exists)
                return (false, Messages.User.i_UserAlreadyExists);

            await _authRepository.InsertUserAsync(request);
            return (true, null);
        }
    }

}


