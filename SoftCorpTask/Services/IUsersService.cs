using SoftCorpTask.Models;

namespace SoftCorpTask.Services;

public interface IUsersService
{
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task<UserModel> RegisterAsync(RegisterUserModel model);
    Task<TokenModel> LoginAsync(LoginModel model);
    Task<TokenModel?> RefreshTokenAsync(RefreshTokenModel model);
}