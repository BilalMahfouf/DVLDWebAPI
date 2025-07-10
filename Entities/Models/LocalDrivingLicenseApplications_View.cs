using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class LocalDrivingLicenseApplications_View
{
    public int LocalDrivingLicenseApplicationID { get; set; }

    public string ClassName { get; set; } = null!;

    public string NationalNo { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime ApplicationDate { get; set; }

    public int? PassedTestCount { get; set; }

    public string? Status { get; set; }
}
