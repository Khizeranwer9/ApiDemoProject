using ApiDemo.Exceptions;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.Extensions.Logging;


namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpGet("users")]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            _logger.LogInformation("Fetching all users");
            var users = await _userService.GetAllUsersAsync();
            if (users == null)
            {
                throw new AppException("Failed to retrieve users");
            }
                
            
                return Ok(users);
            
           
          
        }
        [HttpPost("save/user")]
        public async Task<ActionResult<bool>> SaveUser([FromBody] UserModel user)
        {
            if (user == null)
            {
                _logger.LogWarning("User data is null");
                throw new BadRequestException("User data is null");
            }
            var result = await _userService.SaveUser(user);
            if (!result)
            {
                throw new AppException("Failed to save user");
            }
            return Ok(result);
        }
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            _logger.LogInformation("Fetching user with id {UserId}", id);
            var user = await _userService.GetUserById(id);

            if (user==null)
            {
                _logger.LogWarning("User with id {UserId} not found", id);
                throw new NotFoundException("User not found");
            }
            
            return Ok(user);
        }

        [HttpDelete("delete/user/{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            _logger.LogInformation("Deleting user with id {UserId}", id); 
            var result = await _userService.DeleteUser(id);
            if (!result)
            {
                _logger.LogWarning("User with id {UserId} not found", id);      
                throw new NotFoundException("User not found");
            }
            return Ok(result);
        }
    }
}
