using Core.DTOs.Application;
using Core.DTOs.Test;
using Core.Interfaces.Services.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Tests
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAppointmentController : ControllerBase
    {
        private readonly ITestAppointmentService _service;

        public TestAppointmentController(ITestAppointmentService service)
        {
            _service = service;
        }

        [HttpGet("getById/{id}", Name = "GetTestAppointmentByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<TestAppointmentDTO>> GetTestAppointmentByIDAsync(int id)
        {
            var response = await _service.FindByIDAsync(id);
            return response.HandleResult();
        }


        [HttpPost("create", Name = "CreateTestAppointmentAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<int>> CreateTestAppointmentAsync
            ([FromBody] TestAppointmentDTO request)
        {
            var response = await _service.CreateTestAppointmentAsync(request);
            return response.HandleResult(nameof(GetTestAppointmentByIDAsync), new { Id = response.Data });
        }

        [HttpDelete("delete/{id:int}", Name = "DeleteTestAppointmentAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTestAppointmentAsync(int id)
        {
            var response = await _service.DeleteTestAppointmentAsync(id);
            return response.HandleResult();
        }

        [HttpGet("all", Name = "GetAllTestAppointmentsAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<TestAppointmentDahsboardDTO>>>
            GetAllTestAppointmentsAsync()
        {
            var response = await _service.GetAllTestAppointmentAsync();
            return response.HandleResult();
        }

        [HttpPut("retake-testAppointment/{id:int}", Name = "RetakeTestAppointmentAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> RetakeTestAppointmentAsync(int id, int retakeTestAppointmentID)
        {
            var response = await _service.UpdateTestAppointmentAsync(id, retakeTestAppointmentID);
            return response.HandleResult();
        }
        [HttpPut("lock-testAppointment/{id:int}", Name = "LockTestAppointmentAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
         public async Task<ActionResult> LockTestAppointmentAsync(int id)
         {
            var response = await _service.LockTestAppointment(id);
            return response.HandleResult();
         }


    }
}
