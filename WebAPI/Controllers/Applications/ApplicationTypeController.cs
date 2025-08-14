using BusinessLoginLayer.Services.Applications;
using Core.DTOs.Application;
using Core.DTOs.Application.ApplicationType;
using Core.Interfaces.Services.Applications;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Applications
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationTypeController : ControllerBase
    {
        private readonly ApplicationTypeService _service;

        public ApplicationTypeController(ApplicationTypeService service)
        {
            _service = service;
        }

        [HttpGet("getById/{id:int}", Name = "GetApplicationTypeByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ApplicationTypeDTO>> GetApplicationTypeByIDAsync(int id)
        {
            var response = await _service.FindByIDAsync(id);
            return response.HandleResult();
        }


        [HttpPut("update-fees/{id:int}", Name = "UpdateApplicationTypeFeesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> UpdateApplicationTypeFeesAsync(int id,decimal fees)
        {
            var response = await _service.UpdateFeesAsync(id, fees);
            return response.HandleResult();
        }

        [HttpGet("all", Name = "GetAllApplicationTypesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<ApplicationTypeDTO>>>
            GetAllApplicationTypesAsync()
        {
            var response = await _service.GetAllApplicationTypesAsync();
            return response.HandleResult();
        }
    }
}
