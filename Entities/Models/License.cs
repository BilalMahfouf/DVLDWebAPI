using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class License
{
    public int LicenseID { get; set; }

    public int ApplicationID { get; set; }

    public int DriverID { get; set; }

    public int LicenseClass { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string? Notes { get; set; }

    public decimal PaidFees { get; set; }

    public bool IsActive { get; set; }

    /// <summary>
    /// 1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.
    /// </summary>
    public byte IssueReason { get; set; }

    public int CreatedByUserID { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual ICollection<DetainedLicense> DetainedLicenses { get; set; } = new List<DetainedLicense>();

    public virtual Driver Driver { get; set; } = null!;

    public virtual ICollection<InternationalLicense> InternationalLicenses { get; set; } = new List<InternationalLicense>();

    public virtual LicenseClass LicenseClassNavigation { get; set; } = null!;
}
