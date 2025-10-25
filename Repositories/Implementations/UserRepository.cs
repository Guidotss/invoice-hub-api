using Invoce_Hub.Dtos.User;
using Invoce_Hub.Models;
using Invoce_Hub.Services;

namespace Invoce_Hub.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserService _userService;

        public UserRepository(IUserService userService)
        {
            _userService = userService;
        }

        public Task<User> CreateUser(CreateUserDto createUserDto)
        {
            return _userService.CreateUser(createUserDto);
        }


        public Task<User> GetUserByEmail(string email)
        {
            return _userService.GetUserByEmail(email);
        }

        public Task<User> GetUserById(Guid userId)
        {
            return _userService.GetUserById(userId);
        }
    }
}
