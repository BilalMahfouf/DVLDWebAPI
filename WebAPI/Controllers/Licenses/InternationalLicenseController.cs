using Core.DTOs.License;
using Core.Interfaces.Services.Licenses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Licenses
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternationalLicenseController : ControllerBase
    {
        private readonly IInternationalLicenseService _service;

        public InternationalLicenseController(IInternationalLicenseService service)
        {
            _service = service;
        }

        [HttpGet("getById/{id:int}", Name = "GetInternationalLicenseByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadInternationalLicenseDTO>>
            GetInternationalLicenseByIDAsync(int id)
        {
            var response = await _service.FindByIDAsync(id);
            return response.HandleResult();
        }
        [HttpGet("all", Name = "GetAllInternationalLicensesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReadInternationalLicenseDTO>>>
            GetAllInternationalLicensesAsync()
        {
            var response = await _service.GetAllAsync();
            return response.HandleResult();
        }
        [HttpPost("issueNew", Name = "IssueNewInternationalLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> IssueNewInternationalLicenseAsync
            ([FromBody] InternationalLicenseDTO request)
        {
            var response = await _service.IssueInternationalLicense(request);
            return response.HandleResult(nameof(GetInternationalLicenseByIDAsync), new { Id = response.Data });
        }
          
        [HttpDelete("delete/{id:int}", Name = "DeleteInternationalLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteInternationalLicenseAsync(int id)
        {
            var response = await _service.DeleteInternationalLicenseAsync(id);
            return response.HandleResult();
        }
        [HttpPut("activate/{id:int}", Name = "ActivateInternationalLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ActivateInternationalLicenseAsync(int id)
        {
            var response = await _service.ActivateAsync(id);
            return response.HandleResult();
        }
        [HttpPut("deactivate/{id:int}", Name = "DeActivateInternationalLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeActivateAsync(int id)
        {
            var response = await _service.DeActivateAsync(id);
            return response.HandleResult();
        }


    }
}
