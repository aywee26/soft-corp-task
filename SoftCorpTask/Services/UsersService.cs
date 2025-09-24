using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftCorpTask.Contexts;
using SoftCorpTask.Entities;
using SoftCorpTask.Enums;
using SoftCorpTask.Exceptions;
using SoftCorpTask.Mapping;
using SoftCorpTask.Models;

namespace SoftCorpTask.Services;

public class UsersService : IUsersService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly IConfiguration _configuration;
    private readonly UserMapper _userMapper;

    public UsersService(ApplicationDbContext context, IPasswordService passwordService, IConfiguration configuration)
    {
        _context = context;
        _passwordService = passwordService;
        _configuration = configuration;
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

    public async Task<TokenModel> LoginAsync(LoginModel model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == model.Login);
        
        if (user is null)
            throw new UserNotFoundException();
        
        var expectedPasswordHash = _passwordService.GenerateHash(model.Password, user.PasswordSalt);
        if (expectedPasswordHash != user.PasswordHash)
            throw new InvalidPasswordException();

        var generatedToken = CreateToken(user);
        var result = new TokenModel
        {
            Token = generatedToken,
        };

        return result;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
        };
        
        var secret = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration.GetValue<string>("JsonWebTokenStuff:Secret")!));
        
        var creds = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JsonWebTokenStuff:Issuer"),
            audience: _configuration.GetValue<string>("JsonWebTokenStuff:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}