using Core.DTOs.Application;
using Core.Interfaces.Services.Applications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Applications
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalDrivingLicenseApplicationController : ControllerBase
    {
        private readonly ILocalDrivingLicenseApplicationService _service;

        public LocalDrivingLicenseApplicationController
            (ILocalDrivingLicenseApplicationService service)
        {
            _service = service;
        }

        [HttpGet("getById/{id:int}", Name = "GetByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<LocalDrivingLicenseDTO>> GetByIDAsync(int id)
        {
            var response = await _service.FindLDLAppByIDAsync(id);
            return response.HandleResult();
        }


        [HttpPost("create", Name = "CreateAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> CreateAsync
            ([FromBody] LocalDrivingLicenseDTO request)
        {
            var response = await _service.CreateLDLApplicationAsync(request);
            return response.HandleResult(nameof(GetByIDAsync), new { Id = response.Data });
        }

        [HttpDelete("delete/{id:int}", Name = "DeleteAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            var response = await _service.DeleteLDLApplicationAsync(id);
            return response.HandleResult();
        }

        [HttpGet("all", Name = "GetAllAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<LocalDrivingLicenseApplicationDashboardDTO>>
            GetAllAsync()
        {
            var response = await _service.GetAllAsync();
            return response.HandleResult();
        }
    }
}
