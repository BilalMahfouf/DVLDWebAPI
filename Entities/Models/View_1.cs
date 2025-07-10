using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class View_1
{
    public int LocalDrivingLicenseApplicationID { get; set; }

    public string ClassName { get; set; } = null!;

    public string NationalNo { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string? ThirdName { get; set; }

    public string LastName { get; set; } = null!;

    public DateTime ApplicationDate { get; set; }

    public bool TestResult { get; set; }

    public byte ApplicationStatus { get; set; }
}
