using Invoce_Hub.Dtos.User;
using Invoce_Hub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoce_Hub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ILogger<UserController> logger, IUserRepository userRepository) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly IUserRepository _userRepository = userRepository;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var user = await _userRepository.CreateUser(createUserDto);
                var response = new
                {
                    Ok = true,
                    Message = "User created successfully",
                    Data = new
                    {
                        Name = user.Name,
                        LastName = user.LastName,
                        Email = user.Email,
                        Id = user.Id
                    }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

    }
}
