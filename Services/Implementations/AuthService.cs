using Invoce_Hub.Dtos.Auth;
using Invoce_Hub.Models;
using Invoce_Hub.Repositories;

namespace Invoce_Hub.Services.Implementations;

public class AuthService(IUserRepository userRepository, ILogger<AuthService> logger)
    : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ILogger<AuthService> _logger = logger;
    
    private static bool VerifyPassword(string enteredPassword, string storedPasswordHash)
    {
        
        return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash);
    }

    public async Task<User> Login(LoginDto logindto)
    {
        var checkUserExist = await _userRepository.GetUserByEmail(logindto.Email);
        
        var isPasswordValid = VerifyPassword(logindto.Password, checkUserExist.PasswordHash);
        if (isPasswordValid) return checkUserExist;
        _logger.LogWarning("Invalid password attempt for email: {Email}", logindto.Email);
        return null;
    }
}