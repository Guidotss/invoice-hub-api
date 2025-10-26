using Invoce_Hub.Dtos.Auth;
using Invoce_Hub.Models;
using Invoce_Hub.Services;

namespace Invoce_Hub.Repositories.Implementations;

public class AuthRepository(IAuthService authService) : IAuthRepository
{
    private readonly IAuthService _authService = authService;

    public Task<User> Login(LoginDto logindto)
    {
        return _authService.Login(logindto);
    }
}