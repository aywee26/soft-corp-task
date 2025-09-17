using System.Security.Cryptography;
using System.Text;

namespace SoftCorpTask.Services;

public class SimplePasswordService : IPasswordService
{
    private const int Iterations = 350_000;
    private const int KeySize = 64;
    
    public string GenerateHash(string password, string salt)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Encoding.UTF8.GetBytes(salt),
            Iterations,
            HashAlgorithmName.SHA256,
            KeySize);
        
        var hashedPassword = Convert.ToBase64String(hash);
        
        return hashedPassword;
    }

    public string GenerateSalt()
    {
        var rng = RandomNumberGenerator.Create();
        var salt = new byte[KeySize];
        rng.GetBytes(salt);
        var generatedSalt = Convert.ToBase64String(salt);

        return generatedSalt;
    }
}