using Core.DTOs.Application.ApplicationType;
using Core.DTOs.Person;
using Core.DTOs.User;
using Core.Common;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Application
{
    public class ReadApplicationDTO
    {
        public int ApplicationID { get; set; }

        public int ApplicantPersonID { get; set; }
        public ReadPersonDTO ApplicantPerson { get; set; } = null!;

        public DateTime ApplicationDate { get; set; }

        public Enums.ApplicationTypeEnum ApplicationTypeID { get; set; }
        public ApplicationTypeDTO ApplicationType { get; set; } = null!;

        public Enums.ApplicationStatusEnum ApplicationStatus { get; set; }

        public DateTime LastStatusDate { get; set; }

        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }
        public ReadUserDTO CreatedByUser { get; set; } = null!;

        
        public ReadApplicationDTO()
        {
        }
    }
}
