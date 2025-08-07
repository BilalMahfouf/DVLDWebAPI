using BusinessLoginLayer.Services.Applications;
using BusinessLoginLayer.Services.Tests;
using Core.DTOs.Application.ApplicationType;
using Core.DTOs.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Tests
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTypeController : ControllerBase
    {
        private readonly TestTypeService _service;

        public TestTypeController(TestTypeService service)
        {
            _service = service;
        }

        [HttpGet("getById/{id:int}", Name = "GetByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<TestTypeDTO>> GetByIDAsync(int id)
        {
            var response = await _service.FindByIDAsync(id);
            return response.HandleResult();
        }


        [HttpPut("update-fees/{id:int}", Name = "UpdateFeesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> UpdateFeesAsync(int id, decimal fees)
        {
            var response = await _service.UpdateFeesAsync(id, fees);
            return response.HandleResult();
        }

        [HttpGet("all", Name = "GetAllAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<TestTypeDTO>>>GetAllAsync()
        {
            var response = await _service.GetAllAsync();
            return response.HandleResult();
        }
    }
}
