using Invoce_Hub.Dtos.Auth;
using Invoce_Hub.Dtos.User;
using Invoce_Hub.Repositories;
using Invoce_Hub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoce_Hub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService,IJwtService jwtService ,ILogger<AuthController> logger) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var checkUser =  await authService.Login(loginDto);
                if (checkUser == null)
                {
                    return Unauthorized(new { Ok = false, Message = "Invalid credential." });
                }
                else
                {
                    string token = jwtService.GenerateToken(checkUser.Id, checkUser.Email);
                    var response = new
                    {
                        Ok = true,
                        Message = "Login successful",
                        Token = token,
                        Data = new
                        {
                            Name = checkUser.Name,
                            LastName = checkUser.LastName,
                            Email = checkUser.Email,
                            Id = checkUser.Id
                        }
                    }; 
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during login for email: {Email}", loginDto.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}