using Core.DTOs.Driver;
using Core.DTOs.User;
using Core.Interfaces.Services.Drivers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _service;

        public DriverController(IDriverService service)
        {
            _service = service;
        }
        [HttpGet("getById/{id:int}", Name = "GetDriverByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ReadDriverDTO>> GetDriverByIDAsync(int id)
        {
            var response = await _service.FindByIDAsync(id);
            return response.HandleResult();
        }

        [HttpGet("all", Name = "GetAllDriversAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<DriverDashboardDTO>>>
            GetAllDriversAsync()
        {
            var response = await _service.GetAllDriversAsync();
            return response.HandleResult();
        }

        [HttpPost("create", Name = "CreateDriverAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> CreateDriverAsync([FromBody] DriverDTO userDTO)
        {
            var response = await _service.CreateDriverAsync(userDTO);
            return response.HandleResult(nameof(GetDriverByIDAsync), new { Id = response.Data });
        }

        

        [HttpDelete("delete/{id:int}", Name = "DeleteDriverAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDriverAsync(int id)
        {
            var response = await _service.DeleteDriverAsync(id);
            return response.HandleResult();
        }

        

    }
}
