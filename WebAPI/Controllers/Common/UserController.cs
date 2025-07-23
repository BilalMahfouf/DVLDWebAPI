using Core.DTOs.User;
using Core.Interfaces.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Common
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
        [HttpGet("GetUserById/{id}", Name = "GetUserByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ReadUserDTO>> GetUserByIDAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID.");
            }
            var user = await _userService.FindByIDAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        [HttpGet("All", Name = "GetAllAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }

        [HttpPost("Create", Name = "CreateUserAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ReadUserDTO>> CreateUserAsync([FromBody] CreateUserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User data is required.");
            }
            var userId = await _userService.CreateUserAsync(userDTO);
            var createdUser = await _userService.FindByIDAsync(userId);
            return CreatedAtRoute("GetUserByIDAsync", new { id = userId }, createdUser);
        }

        [HttpPut("Update/{userID}", Name = "UpdateUserAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> UpdateUserAsync(int userID, [FromBody] UpdateUserDTO userDTO)
        {
            if (userDTO == null || userID <= 0)
            {
                return BadRequest("Invalid user data or ID.");
            }
            var updated = await _userService.UpdateUserAsync(userID, userDTO);
            if (!updated)
            {
                return NotFound($"User with ID {userID} not found.");
            }
            return Ok();
        }

        //[HttpDelete("Delete/{id}", Name = "DeleteUserAsync")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]

    }
}

