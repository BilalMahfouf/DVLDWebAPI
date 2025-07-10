using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class TestAppointments_View
{
    public int TestAppointmentID { get; set; }

    public int LocalDrivingLicenseApplicationID { get; set; }

    public string TestTypeTitle { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public DateTime AppointmentDate { get; set; }

    public decimal PaidFees { get; set; }

    public string FullName { get; set; } = null!;

    public bool IsLocked { get; set; }
}
