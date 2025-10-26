using Invoce_Hub.Data;
using Invoce_Hub.Dtos.User;
using Invoce_Hub.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt;
namespace Invoce_Hub.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly InvoiceHubDbContext _context;
        public UserService(ILogger<UserService> logger, InvoiceHubDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public async Task<User> CreateUser(CreateUserDto createUserDto)
        {
            var checkUser = await GetUserByEmail(createUserDto.Email);
            if (checkUser != null)
            {
                throw new Exception("User with this email already exists.");
            }
            var newUser = new User
            {
                Name = createUserDto.Name,
                LastName = "",
                Email = createUserDto.Email,
                PasswordHash = HashPassword(createUserDto.Password),
            };
            
            _context.Users.Add(newUser);
            await  _context.SaveChangesAsync();
            return newUser; 
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                return checkUser; 
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user by email: {Email}", email);
                throw;
            }
        }

        public Task<User> GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}