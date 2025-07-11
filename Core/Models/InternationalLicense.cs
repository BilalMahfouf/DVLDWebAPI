using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class InternationalLicense:IEntity
{
    public int InternationalLicenseID { get; set; }

    int IEntity.ID
    {
        get => InternationalLicenseID;
        set => InternationalLicenseID = value;
    }
    public int ApplicationID { get; set; }

    public int DriverID { get; set; }

    public int IssuedUsingLocalLicenseID { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool IsActive { get; set; }

    public int CreatedByUserID { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;

    public virtual License IssuedUsingLocalLicense { get; set; } = null!;
}
