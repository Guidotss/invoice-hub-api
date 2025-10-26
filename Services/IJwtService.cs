namespace Invoce_Hub.Services;

public interface IJwtService
{
    string GenerateToken(Guid userId, string email);
    bool ValidateToken(string token); 
}