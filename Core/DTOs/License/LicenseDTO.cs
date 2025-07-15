using Core.Common;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.License
{
    public class LicenseDTO
    {
        public int ApplicationID { get; set; }

        public int DriverID { get; set; }

        public Enums.LicenseClassTypeEnum LicenseClass { get; set; }

        public string? Notes { get; set; }

        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

       
        public LicenseDTO() { }
    }
}
