using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Test
{
    public class TestAppointmentDahsboardDTO
    {
        public int TestAppointmentID { get; set; }

        public int LocalDrivingLicenseApplicationID { get; set; }

        public string TestTypeTitle { get; set; } = null!;

        public string ClassName { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }

        public decimal PaidFees { get; set; }

        public string FullName { get; set; } = null!;

        public bool IsLocked { get; set; }
        public TestAppointmentDahsboardDTO() { }
    }
}
