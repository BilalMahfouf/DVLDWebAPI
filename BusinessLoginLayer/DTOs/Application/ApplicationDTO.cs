using Core.Common;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.DTOs.Application
{
    public class ApplicationDTO
    {
        public int ApplicationID { get; set; }

        public int ApplicantPersonID { get; set; }

        public DateTime ApplicationDate { get; set; }

        public Enums.ApplicationTypeEnum ApplicationTypeID { get; set; }

        /// <summary>
        /// 1-New 2-Cancelled 3-Completed
        /// </summary>
        public byte ApplicationStatus { get; set; }

        public DateTime LastStatusDate { get; set; }

        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public ApplicationDTO() { }
    }
}
