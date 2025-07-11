using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccessLayer;

public partial class LocalDrivingLicenseApplication:IEntity
{
    public int LocalDrivingLicenseApplicationID { get; set; }
    int IEntity.ID
    {
        get => LocalDrivingLicenseApplicationID;
        set => LocalDrivingLicenseApplicationID = value;
    }
    public int ApplicationID { get; set; }

    public int LicenseClassID { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual LicenseClass LicenseClass { get; set; } = null!;

    public virtual ICollection<TestAppointment> TestAppointments { get; set; } = new List<TestAppointment>();
}
