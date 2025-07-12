using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Country
{
    public class ReadCountryDTO
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; } = string.Empty;

        public ReadCountryDTO(int countryID, string countryName)
        {
            CountryID = countryID;
            CountryName = countryName;
        }
        public ReadCountryDTO() { }
    }
}
