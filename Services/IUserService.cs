using Invoce_Hub.Dtos.User;
using Invoce_Hub.Models;

namespace Invoce_Hub.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(Guid userId); 
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(CreateUserDto createUserDto);
    }
}
