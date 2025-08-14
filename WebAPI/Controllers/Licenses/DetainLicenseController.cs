using Core.DTOs.Detain;
using Core.DTOs.License;
using Core.Interfaces.Services.Licenses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Licenses
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetainLicenseController : ControllerBase
    {
        private readonly IDetainLicenseService _service;

        public DetainLicenseController(IDetainLicenseService service)
        {
            _service = service;
        }
        [HttpGet("getById/{id:int}", Name = "GetDetainedLicenseByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DetainLicenseDTO>> GetDetainedLicenseByIDAsync(int id)
        {
            var response = await _service.FindAsync(id);
            return response.HandleResult();
        }
        [HttpGet("all", Name = "GetAllDetainedLicensesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DetainedLicenseDashboardDTO>>>
            GetAllDetainedLicensesAsync()
        {
            var response = await _service.GetAllAsync();
            return response.HandleResult();
        }
        [HttpPost("detain", Name = "DetainLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> DetainLicenseAsync
            ([FromBody] DetainLicenseDTO request)
        {
            var response = await _service.CreateDetainedLicenseAsync(request);
            return response.HandleResult(nameof(GetDetainedLicenseByIDAsync), new { Id = response.Data });
        }

        [HttpDelete("delete/{id:int}", Name = "DeleteDetainLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDetainLicenseAsync(int id)
        {
            var response = await _service.DeleteDetainedLicenseAsync(id);
            return response.HandleResult();
        }
        
        [HttpPut("release/{id:int}", Name = "ReleaseDetainedLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ReleaseDetainedLicenseAsync(
            [FromBody] UpdateDetainedLicenseDTO request)
        {
            var response = await _service.ReleaseLicenseAsync(request);
            return response.HandleResult();
        }

    }
}
