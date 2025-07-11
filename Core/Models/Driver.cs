using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class Driver:IEntity
{
    public int DriverID { get; set; }

    int IEntity.ID
    {
        get => DriverID;
        set => DriverID = value;
    }
    public int PersonID { get; set; }

    public int CreatedByUserID { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual ICollection<InternationalLicense> InternationalLicenses { get; set; } = new List<InternationalLicense>();

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    public virtual Person Person { get; set; } = null!;
}
