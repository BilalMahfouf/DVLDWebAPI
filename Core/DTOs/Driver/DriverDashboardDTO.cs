using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Driver
{
    public class DriverDashboardDTO
    {
        public int DriverID { get; set; }

        public int PersonID { get; set; }

        public string NationalNo { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public int? NumberOfActiveLicenses { get; set; }
        public DriverDashboardDTO() { }
    }
}
