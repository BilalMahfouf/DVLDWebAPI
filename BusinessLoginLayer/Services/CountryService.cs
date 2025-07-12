using AutoMapper;
using Core.DTOs.Country;
using Core.Interfaces.Repositories.Common;
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
        public async Task<IEnumerable<ReadCountryDTO>> GetAllCountriesAsync()
        {
            var countries = await _repo.GetAllAsync();
            var readCounteisDTO=_mapper.Map<IEnumerable<ReadCountryDTO>>(countries);
            return readCounteisDTO;
        }
    }
}
