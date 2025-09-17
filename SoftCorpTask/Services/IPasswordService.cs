namespace SoftCorpTask.Services;

public interface IPasswordService
{
    string GenerateHash(string password, string salt);
    string GenerateSalt();
}