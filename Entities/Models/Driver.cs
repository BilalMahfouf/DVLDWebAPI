using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class Driver
{
    public int DriverID { get; set; }

    public int PersonID { get; set; }

    public int CreatedByUserID { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual ICollection<InternationalLicense> InternationalLicenses { get; set; } = new List<InternationalLicense>();

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    public virtual Person Person { get; set; } = null!;
}
