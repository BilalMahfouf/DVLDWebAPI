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

        public ReadApplicationDTO(int applicationID, int applicantPersonID,
            ReadPersonDTO applicantPerson, DateTime applicationDate,
            Enums.ApplicationTypeEnum applicationTypeID, ApplicationTypeDTO
            applicationType, Enums.ApplicationStatusEnum applicationStatus,
            DateTime lastStatusDate, decimal paidFees, int createdByUserID,
            ReadUserDTO createdByUser)
        {
            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            ApplicantPerson = applicantPerson;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;
            ApplicationType = applicationType;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            CreatedByUser = createdByUser;
        }

        public ReadApplicationDTO()
        {
        }
    }
}
