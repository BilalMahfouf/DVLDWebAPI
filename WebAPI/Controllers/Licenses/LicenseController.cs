using Core.DTOs.License;
using Core.Interfaces.Services.Licenses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Licenses
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _service;
        public LicenseController(ILicenseService licenseService)
        {
            _service = licenseService;
        }
        [HttpGet("getById/{id:int}", Name = "GetLicenseByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadLicenseDTO>> GetLicenseByIDAsync(int id)
        {
            var response = await _service.FindByIDAsync(id);
            return response.HandleResult();
        }
        [HttpGet("all", Name = "GetAllLicensesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReadLicenseDTO>>> GetAllLicensesAsync()
        {
            var response = await _service.GetAllLicenseAsync();
            return response.HandleResult();
        }
        [HttpPost("issueNew", Name = "IssueNewDrivingLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> IssueNewDrivingLicenseAsync
            ([FromBody] LicenseDTO licenseDTO)
        {
            var response = await _service.IssueNewDrivingLicenseAsync(licenseDTO);
            return response.HandleResult(nameof(GetLicenseByIDAsync), new { Id = response.Data });
        }
        [HttpPut("renew/{oldLicenseID:int}", Name = "RenewLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> RenewLicenseAsync
            (int oldLicenseID,[FromBody] LicenseDTO licenseDTO)
        {
            var response = await _service.RenewLicenseAsync(oldLicenseID, licenseDTO);
            return response.HandleResult(nameof(GetLicenseByIDAsync), new { Id = response.Data });
        }
        [HttpPut("issueReplacementForLost/{oldLicenseID:int}", Name = "IssueReplacementForLostLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> IssueReplacementForLostLicenseAsync
            (int oldLicenseID, [FromBody] LicenseDTO licenseDTO)
        {
            var response = await _service.IssueReplacementForLostLicenseAsync(oldLicenseID, licenseDTO);
            return response.HandleResult(nameof(GetLicenseByIDAsync), new { Id = response.Data });
        }
        [HttpPut("issueReplacementForDamaged/{oldLicenseID:int}", Name = "IssueReplacementForDamagedLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> IssueReplacementForDamagedLicenseAsync
            (int oldLicenseID, [FromBody] LicenseDTO licenseDTO)
        {
            var response = await _service.IssueReplacementForDamagedLicenseAsync(oldLicenseID, licenseDTO);
            return response.HandleResult(nameof(GetLicenseByIDAsync), new { Id = response.Data });
        }
        [HttpDelete("delete/{id:int}", Name = "DeleteLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLicenseAsync(int id)
        {
            var response = await _service.DeleteLicenseAsync(id);
            return response.HandleResult();
        }
        [HttpPut("activate/{id:int}", Name = "ActivateLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ActivateLicenseAsync(int id)
        {
            var response = await _service.ActivateLicenseAsync(id);
            return response.HandleResult();
        }
        [HttpPut("deactivate/{id:int}", Name = "DeActivateLicenseAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeActivateLicenseAsync(int id)
        {
            var response = await _service.DeActivateLicenseAsync(id);
            return response.HandleResult();
        }

    }
}
