using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Application.ApplicationType
{
    public class ApplicationTypeDTO
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; } = null!;
        public decimal ApplicationFees { get; set; }

        public ApplicationTypeDTO(int applicationTypeID, string applicationTypeTitle
            , decimal applicationFees)
        {
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationFees = applicationFees;
        }
        public ApplicationTypeDTO() { }
    }
   
}
