using Duckov.Api.Logins.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Duckov.Api.Logins.Controllers;

[ApiController]
[Route("api/logins")]
public class LoginsController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IJwtService _jwtService;

    public LoginsController(ILoginService loginService, IJwtService jwtService)
    {
        _loginService = loginService;
        _jwtService = jwtService;
    }

    [HttpPost]
    public ActionResult Login([FromBody] LoginRequest request)
    {
        if (!_loginService.IsValidUser(request.Email, request.Password))
        {
            return Unauthorized();
        }

        var token = _jwtService.GenerateToken(request.Email);
        return Ok(new { Token = token });
    }
}