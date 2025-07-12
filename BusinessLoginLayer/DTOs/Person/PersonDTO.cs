using BusinessLoginLayer.DTOs.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLoginLayer.DTOs.Person
{
    public class PersonDTO
    {
        public string NationalNo { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string SecondName { get; set; } = null!;

        public string? ThirdName { get; set; }

        public string LastName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// 0 Male , 1 Femail
        /// </summary>
        public byte Gender { get; set; }

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? Email { get; set; }

        public int NationalityCountryID { get; set; }
        public string? ImagePath { get; set; }

        public PersonDTO(string nationalNo, string firstName, string secondName,
            string? thirdName, string lastName, DateTime dateOfBirth, byte gender,
            string address, string phone, string? email, int nationalityCountryID,
            string? imagePath)
        {
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            Phone = phone;
            Email = email;
            NationalityCountryID = nationalityCountryID;
            ImagePath = imagePath;
        }
        public PersonDTO() { }  
    }
}
