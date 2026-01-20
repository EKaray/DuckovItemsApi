namespace Duckov.Api.Logins.Services;

public interface IJwtService
{
    public string GenerateToken(string userName);
}