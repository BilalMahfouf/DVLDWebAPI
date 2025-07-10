using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class TestType
{
    public int TestTypeID { get; set; }

    public string TestTypeTitle { get; set; } = null!;

    public string TestTypeDescription { get; set; } = null!;

    public decimal TestTypeFees { get; set; }

    public virtual ICollection<TestAppointment> TestAppointments { get; set; } = new List<TestAppointment>();
}
