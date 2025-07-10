using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class Drivers_View
{
    public int DriverID { get; set; }

    public int PersonID { get; set; }

    public string NationalNo { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int? NumberOfActiveLicenses { get; set; }
}
