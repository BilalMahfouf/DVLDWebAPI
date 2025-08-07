using Core.DTOs.Application;
using Core.DTOs.Test;
using Core.Interfaces.Services.Applications;
using Core.Interfaces.Services.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Tests
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _service;

        public TestController(ITestService service)
        {
            _service = service;
        }

        [HttpGet("getById/{id:int}", Name = "GetByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<TestDTO>> GetByIDAsync(int id)
        {
            var response = await _service.FindByIDAsync(id);
            return response.HandleResult();
        }


        [HttpPost("create", Name = "CreateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> CreateAsync([FromBody] TestDTO request)
        {
            var response = await _service.CreateTestAsync(request);
            return response.HandleResult(nameof(GetByIDAsync), new { Id = response.Data });
        }

        [HttpDelete("delete/{id:int}", Name = "DeleteAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var response = await _service.DeleteTestAsync(id);
            return response.HandleResult();
        }

    }
}
