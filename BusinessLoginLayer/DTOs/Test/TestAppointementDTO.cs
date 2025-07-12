using Core.Common;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.DTOs.Test
{
    public class TestAppointmentDTO
    {
        public int TestAppointmentID { get; set; }
        public Enums.TestTypeEnum TestType { get; set; }

        public int LocalDrivingLicenseApplicationID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public bool IsLocked { get; set; }

        public int? RetakeTestApplicationID { get; set; }

        public TestAppointmentDTO(int testAppointmentID, Enums.TestTypeEnum testType
            , int localDrivingLicenseApplicationID, DateTime appointmentDate,
            decimal paidFees, int createdByUserID, bool isLocked
            , int? retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestType = testType;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;
        }
        public TestAppointmentDTO() { } 
    }
}
