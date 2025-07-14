using BusinessLoginLayer.Services;
using Core.DTOs.Country;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryService _countryService;

        public CountryController(CountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("All")]

        public async Task<ActionResult<IEnumerable<ReadCountryDTO>>>
            GetAllAsync()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            if(countries is null || !countries.Any())
            {
                return NotFound("No countries found.");
            }
            return Ok(countries);
        }
    }
}
