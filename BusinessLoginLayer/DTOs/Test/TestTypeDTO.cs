using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.DTOs.Test
{
    public class TestTypeDTO
    {
        public int TestTypeID { get; set; }

        public string TestTypeTitle { get; set; } = null!;

        public string TestTypeDescription { get; set; } = null!;

        public decimal TestTypeFees { get; set; }

        public TestTypeDTO(int testTypeID, string testTypeTitle, 
            string testTypeDescription, decimal testTypeFees)
        {
            TestTypeID = testTypeID;
            TestTypeTitle = testTypeTitle;
            TestTypeDescription = testTypeDescription;
            TestTypeFees = testTypeFees;
        }
        public TestTypeDTO() { }

    }
}
