using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class LocalDrivingLicenseApplication
{
    public int LocalDrivingLicenseApplicationID { get; set; }

    public int ApplicationID { get; set; }

    public int LicenseClassID { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual LicenseClass LicenseClass { get; set; } = null!;

    public virtual ICollection<TestAppointment> TestAppointments { get; set; } = new List<TestAppointment>();
}
