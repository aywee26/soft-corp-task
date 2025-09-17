using SoftCorpTask.Contexts;
using SoftCorpTask.Entities;
using SoftCorpTask.Enums;
using SoftCorpTask.Mapping;
using SoftCorpTask.Models;

namespace SoftCorpTask.Services;

public class UsersService : IUsersService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly UserMapper _userMapper;

    public UsersService(ApplicationDbContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
        _userMapper = new UserMapper();
    }

    public async Task<UserModel> RegisterAsync(RegisterUserModel model)
    {
        var salt = _passwordService.GenerateSalt();
        var passwordHash = _passwordService.GenerateHash(model.Password, salt);

        var entityToSave = new User
        {
            Id = Guid.NewGuid(),
            Login = model.Login,
            PasswordSalt = salt,
            PasswordHash = passwordHash,
            FullName = model.FullName,
            UserRole = UserRole.HumanResources
        };
        
        _context.Users.Add(entityToSave);
        await _context.SaveChangesAsync();
        
        var modelToReturn = _userMapper.MapEntityToModel(entityToSave);
        
        return modelToReturn;
    }
}