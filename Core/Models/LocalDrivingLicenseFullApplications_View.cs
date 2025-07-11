using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class LocalDrivingLicenseFullApplications_View
{
    public int ApplicationID { get; set; }

    public int ApplicantPersonID { get; set; }

    public DateTime ApplicationDate { get; set; }

    public int ApplicationTypeID { get; set; }

    public byte ApplicationStatus { get; set; }

    public DateTime LastStatusDate { get; set; }

    public decimal PaidFees { get; set; }

    public int CreatedByUserID { get; set; }

    public int LocalDrivingLicenseApplicationID { get; set; }

    public int LicenseClassID { get; set; }
}
