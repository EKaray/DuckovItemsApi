namespace Duckov.Api.Logins.Services;

public interface ILoginService
{
    public bool IsValidUser(string email, string password);
}