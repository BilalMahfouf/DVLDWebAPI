using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class Test
{
    public int TestID { get; set; }

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
