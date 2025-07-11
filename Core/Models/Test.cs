using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class Test:IEntity
{
    public int TestID { get; set; }
    int IEntity.ID
    {
        get => TestID;
        set => TestID = value;
    }
    public int TestAppointmentID { get; set; }

    /// <summary>
    /// 0 - Fail 1-Pass
    /// </summary>
    public bool TestResult { get; set; }

    public string? Notes { get; set; }

    public int CreatedByUserID { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual TestAppointment TestAppointment { get; set; } = null!;
}
