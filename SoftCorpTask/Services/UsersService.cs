using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
    private const int ExpireMinutes = 10;

    public UsersService(ApplicationDbContext context, IPasswordService passwordService, IConfiguration configuration)
    {
        _context = context;
        _passwordService = passwordService;
        _configuration = configuration;
        _userMapper = new UserMapper();
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        var userList = await _context.Users.ToListAsync();
        var mappedList = userList.Select(_userMapper.MapEntityToModel);
        return mappedList;
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
            UserRole = UserRole.HumanResources,
            WorkGroupId = model.WorkGroupId
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

        var result = await GenerateTokenModelAsync(user);
        
        return result;
    }

    public async Task<TokenModel?> RefreshTokenAsync(RefreshTokenModel model)
    {
        var refreshTokenEntity = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == model.UserId);
        if (refreshTokenEntity is null)
            throw new UserNotFoundException();

        if (refreshTokenEntity.Token != model.RefreshToken
            || refreshTokenEntity.ExpiresAt < DateTimeOffset.UtcNow)
            throw new InvalidRefreshTokenException();

        var user = await _context.Users.FirstAsync(x => x.Id == refreshTokenEntity.UserId);
        var result = await GenerateTokenModelAsync(user);
        
        return result;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim("WorkGroupId", (user.WorkGroupId is not null ? user.WorkGroupId.Value.ToString() : string.Empty)),
            new Claim(ClaimTypes.Role, user.UserRole.ToString())
        };
        
        var secret = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration.GetValue<string>("JsonWebTokenStuff:Secret")!));
        
        var creds = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JsonWebTokenStuff:Issuer"),
            audience: _configuration.GetValue<string>("JsonWebTokenStuff:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(ExpireMinutes),
            signingCredentials: creds);
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    private async Task<string> CreateRefreshTokenAsync(User user)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var refreshToken = Convert.ToBase64String(randomNumber);
        
        var refreshTokenEntity = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id);
        if (refreshTokenEntity is not null)
        {
            refreshTokenEntity.Token = refreshToken;
            refreshTokenEntity.ExpiresAt = DateTime.UtcNow.AddMinutes(ExpireMinutes * 2);
            _context.RefreshTokens.Update(refreshTokenEntity);
        }
        else
        {
            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(ExpireMinutes * 2),
            };
            _context.RefreshTokens.Add(newRefreshToken);
        }

        await _context.SaveChangesAsync();
        return refreshToken;
    }

    private async Task<TokenModel> GenerateTokenModelAsync(User user)
    {
        var generatedToken = CreateToken(user);
        var generatedRefreshToken = await CreateRefreshTokenAsync(user);
        var result = new TokenModel
        {
            Token = generatedToken,
            RefreshToken = generatedRefreshToken,
        };

        return result;
    }
}