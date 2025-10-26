using Invoce_Hub.Dtos.Auth;
using Invoce_Hub.Models;

namespace Invoce_Hub.Repositories;

public interface IAuthRepository
{
    Task<User> Login(LoginDto logindto);
}