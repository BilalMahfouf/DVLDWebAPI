using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.License
{
    public class LicenseClassDTO
    {
        public int LicenseClassID { get; set; }

        public string ClassName { get; set; } = null!;

        public string ClassDescription { get; set; } = null!;

        /// <summary>
        /// Minmum age allowed to apply for this license
        /// </summary>
        public byte MinimumAllowedAge { get; set; }

        /// <summary>
        /// How many years the licesnse will be valid.
        /// </summary>
        public byte DefaultValidityLength { get; set; }

        public decimal ClassFees { get; set; }

    }
}
