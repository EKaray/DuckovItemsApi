
namespace Duckov.Api.Logins.Services;

public class LoginService : ILoginService
{
    public bool IsValidUser(string email, string password)
    {
        if (email != "Admin" || password != "Admin")
        {
            return false;
        }

        return true;
    }
}