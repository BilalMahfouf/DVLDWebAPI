using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Driver
{
    public class ReadDriverDTO
    {
        public int DriverID { get; set; }

        public int PersonID { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime CreatedDate { get; set; }

        public ReadDriverDTO() { }

    }

}
