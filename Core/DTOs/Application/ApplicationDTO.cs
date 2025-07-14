using Core.Common;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Application
{
    public class ApplicationDTO
    {
        public int ApplicationID { get; set; }

        public int ApplicantPersonID { get; set; }

        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public ApplicationDTO() { }
    }
}
