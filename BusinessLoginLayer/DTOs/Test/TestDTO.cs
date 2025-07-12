using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.DTOs.Test
{
    public class TestDTO
    {
        public int TestID { get; set; }

        public int TestAppointmentID { get; set; }

        /// <summary>
        /// 0 - Fail 1-Pass
        /// </summary>
        public bool TestResult { get; set; }

        public string? Notes { get; set; }

        public int CreatedByUserID { get; set; }

        public TestDTO(int testID, int testAppointmentID, bool testResult
            , string? notes, int createdByUserID)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserID;
        }
        public TestDTO()
        {
        }
    }
}
