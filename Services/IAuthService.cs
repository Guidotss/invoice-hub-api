using Invoce_Hub.Dtos.Auth;
using Invoce_Hub.Models;

namespace Invoce_Hub.Services;

public interface IAuthService
{
    Task<User> Login(LoginDto logindto);
}