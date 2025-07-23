using AutoMapper;
using Core.Common;
using Core.DTOs.Country;
using Core.Interfaces.Repositories.Common;
using Core.Shared;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.Services
{
    public class CountryService
    {
        private readonly IReadUpdateRepository<Country> _repo;
        private readonly IMapper _mapper;

        public CountryService(IReadUpdateRepository<Country> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<ReadCountryDTO>>> GetAllCountriesAsync()
        {
           try
            {
                var countries = await _repo.GetAllAsync();
                if (countries is null || !countries.Any())
                {
                    return Result<IEnumerable<ReadCountryDTO>>.
                        Failure("No countries found.", Enums.ErrorType.NotFound);
                }
                return Result<IEnumerable<ReadCountryDTO>>.
                    Success(_mapper.Map<IEnumerable<ReadCountryDTO>>(countries));
            }
            catch(Exception ex)
            {
                return Result<IEnumerable<ReadCountryDTO>>.
                    Failure($"An error occurred while retrieving data from the DB: {ex.Message}",
                    Enums.ErrorType.InternalServerError);
            }
        }
    }
}
