using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("users")]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpPost("save/user")]
        public async Task<ActionResult<bool>> SaveUser([FromBody] UserModel user)
        {
            var result = await _userService.SaveUser(user);
            if (result)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save user");
        }
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("delete/user/{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete user");
        }
    }
}
