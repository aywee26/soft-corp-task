using SoftCorpTask.Models;

namespace SoftCorpTask.Services;

public interface IUsersService
{
    Task<UserModel> RegisterAsync(RegisterUserModel model);
    Task<TokenModel> LoginAsync(LoginModel model);
}