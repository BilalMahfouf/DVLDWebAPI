using Core.DTOs.Person;
using Core.Interfaces.Services.People;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetPersonById/{id}", Name = "GetPersonByIDAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ReadPersonDTO>> GetPersonByIDAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid person ID.");
            }
            var person = await _personService.FindAsync(id);
            if (person == null)
            {
                return NotFound($"Person with ID {id} not found.");
            }
            return Ok(person);
        }

        [HttpGet("GetPersonByNationalNo/{nationalNo}", Name = "GetPersonByNationalNoAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ReadPersonDTO>> GetPersonByNationalNoAsync
            (string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                return BadRequest("Invalid person nationalNo.");
            }
            var person = await _personService.FindAsync(nationalNo);
            if (person == null)
            {
                return NotFound($"Person with nationalNo {nationalNo} not found.");
            }
            return Ok(person);
        }

        [HttpGet("GetAll", Name = "GetAllAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<ReadPersonDTO>>> GetAllAsync()
        {
            var persons = await _personService.GetAllAsync();
            if (persons == null || !persons.Any())
            {
                return NotFound("No persons found.");
            }
            return Ok(persons);
        }

        [HttpGet("IsExistByID/{id}", Name = "IsExistAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<bool>> IsExistAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid person ID.");
            }
            var isExist = await _personService.IsExistAsync(id);
            return Ok(isExist);
        }

        [HttpGet("IsExistByNationalNo/{nationalNo}", Name = "IsExistAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<bool>> IsExistAsync(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                return BadRequest("Invalid person nationalNo.");
            }
            var isExist = await _personService.IsExistAsync(nationalNo);
            return Ok(isExist);
        }

        [HttpDelete("{id}", Name = "DeleteAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid person ID.");
            }
            var isDeleted = await _personService.DeletePersonAsync(id);
            if (!isDeleted)
            {
                return Conflict($"Person with ID {id} can't be deleted.");
            }
            return Ok(isDeleted);
        }

        [HttpPost("Create", Name = "CreatePersonAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadPersonDTO>> CreatePersonAsync([FromBody] PersonDTO personDTO)
        {

            var personId = await _personService.CreatePersonAsync(personDTO);
            var createdPerson = await _personService.FindAsync(personId);
            return CreatedAtRoute("GetPersonByIDAsync", new { id = personId }
            , createdPerson);
        }

        [HttpPut("{id}", Name = "UpdateAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<bool>> UpdateAsync(int id, [FromBody] PersonDTO personDTO)
        {
            if (id <= 0 || personDTO is null)
            {
                return BadRequest("Invalid person ID or data.");
            }
            var isUpdated = await _personService.UpdatePersonAsync(id, personDTO);
            if (!isUpdated)
            {
                return Conflict($"Person with ID {id} can't be updated.");
            }
            return Ok(isUpdated);




        }
    }
}
