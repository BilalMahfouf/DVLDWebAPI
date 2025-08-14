using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class LicenseClass:IEntity
{
    public int LicenseClassID { get; set; }
    public int ID
    {
        get => LicenseClassID;
        set => LicenseClassID = value;
    }
    public string ClassName { get; set; } = null!;

    public string ClassDescription { get; set; } = null!;

    /// <summary>
    /// Minmum age allowed to apply for this license
    /// </summary>
    public byte MinimumAllowedAge { get; set; }

    /// <summary>
    /// How many years the licesnse will be valid.
    /// </summary>
    public byte DefaultValidityLength { get; set; }

    public decimal ClassFees { get; set; }

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    public virtual ICollection<LocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; } = new List<LocalDrivingLicenseApplication>();
}
