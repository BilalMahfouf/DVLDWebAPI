using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Application
{
    public class LocalDrivingLicenseApplicationDashboardDTO
    {
        public int LocalDrivingLicenseApplicationID { get; set; }

        public string ClassName { get; set; } = null!;

        public string NationalNo { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public DateTime ApplicationDate { get; set; }

        public int? PassedTestCount { get; set; }

        public string? Status { get; set; }
        public LocalDrivingLicenseApplicationDashboardDTO() { }
    }
}
