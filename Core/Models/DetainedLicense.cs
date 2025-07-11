using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class DetainedLicense:IEntity
{
    public int DetainID { get; set; }

    int IEntity.ID
    {
        get => DetainID;
        set => DetainID = value;
    }
    public int LicenseID { get; set; }

    public DateTime DetainDate { get; set; }

    public decimal FineFees { get; set; }

    public int CreatedByUserID { get; set; }

    public bool IsReleased { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? ReleasedByUserID { get; set; }

    public int? ReleaseApplicationID { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual License License { get; set; } = null!;

    public virtual Application? ReleaseApplication { get; set; }

    public virtual User? ReleasedByUser { get; set; }
}
