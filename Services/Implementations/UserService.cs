using Invoce_Hub.Data;
using Invoce_Hub.Dtos.User;
using Invoce_Hub.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<User> CreateUser(CreateUserDto createUserDto)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}