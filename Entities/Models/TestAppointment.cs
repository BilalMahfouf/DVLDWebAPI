using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class TestAppointment
{
    public int TestAppointmentID { get; set; }

    public int TestTypeID { get; set; }

    public int LocalDrivingLicenseApplicationID { get; set; }

    public DateTime AppointmentDate { get; set; }

    public decimal PaidFees { get; set; }

    public int CreatedByUserID { get; set; }

    public bool IsLocked { get; set; }

    public int? RetakeTestApplicationID { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual LocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; set; } = null!;

    public virtual Application? RetakeTestApplication { get; set; }

    public virtual TestType TestType { get; set; } = null!;

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
