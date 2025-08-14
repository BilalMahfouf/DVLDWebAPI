using Core.DTOs.Person;
using Core.Interfaces.Services.People;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using WebAPI.Controllers.Extensions;

namespace WebAPI.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("GetPersonById/{id:int}", Name = "GetPersonByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ReadPersonDTO>> GetPersonByIDAsync(int id)
        {
            var person = await _personService.FindAsync(id);
            return person.HandleResult();
        }

        [HttpGet("GetPersonByNationalNo/{nationalNo}", Name = "GetPersonByNationalNoAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ReadPersonDTO>> GetPersonByNationalNoAsync
            (string nationalNo)
        {
            var person=await _personService.FindAsync(nationalNo);
            return person.HandleResult();
        }

        [HttpGet("GetAll", Name = "GetAllPeopleAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<ReadPersonDTO>>> GetAllPeopleAsync()
        {
            var persons = await _personService.GetAllAsync();
            return persons.HandleResult();
        }

        

        [HttpDelete("{id:int}", Name = "DeleteAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> DeleteAsync(int id)
        {
           var isDeleted=await _personService.DeletePersonAsync(id);
            return isDeleted.HandleResult();
        }

        [HttpPost("Create", Name = "CreatePersonAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> CreatePersonAsync([FromBody] PersonDTO personDTO)
        {

            var response = await _personService.CreatePersonAsync(personDTO);
            return response.HandleResult(nameof(GetPersonByIDAsync), new {personId= response.Data});
        }

        [HttpPut("{id}", Name = "UpdateAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> UpdateAsync(int id, [FromBody] PersonDTO personDTO)
        {
            var response = await _personService.UpdatePersonAsync(id, personDTO);
            return response.HandleResult();
        }
    }
}
