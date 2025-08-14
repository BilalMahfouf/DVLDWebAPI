using Core.DTOs.User;
using Core.Interfaces.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpGet("getById/{id:int}", Name = "GetUserByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ReadUserDTO>> GetUserByIDAsync(int id)
        {
           var response=await _service.FindByIDAsync(id);
            return response.HandleResult();
        }

        [HttpGet("all", Name = "GetAllUsersAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> GetAllUsersAsync()
        {
           var response = await _service.GetAllAsync();
            return response.HandleResult();    
        }

        [HttpPost("create", Name = "CreateUserAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> CreateUserAsync([FromBody] CreateUserDTO userDTO)
        {
            var response= await _service.CreateUserAsync(userDTO);
            return response.HandleResult(nameof(GetUserByIDAsync), new {Id=response.Data});
        }

        [HttpPut("update/{id:int}", Name = "UpdateUserAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> UpdateUserAsync(int userID, [FromBody] UpdateUserDTO userDTO)
        {
            var response = await _service.UpdateUserAsync(userID, userDTO);
            return response.HandleResult();
        }

        [HttpDelete("delete/{id:int}", Name = "DeleteUserAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var response = await _service.DeleteUserAsync(id);
            return response.HandleResult();
        }

        [HttpPut("activate/{id:int}", Name = "ActivateUserAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> ActivateUserAsync(int userID)
        {
            var response = await _service.ActivateAsync(userID);
            return response.HandleResult();
        }
        [HttpPut("deactivate/{id:int}", Name = "DeActivateUserAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> DeActivateUserAsync(int userID)
        {
            var response = await _service.DeActivateAsync(userID);
            return response.HandleResult();
        }

    }
}

