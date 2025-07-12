using AutoMapper;
using Core.DTOs.License;
using DataAccessLayer;

namespace BusinessLoginLayer.Profiles
{
    public class InternationalLicenseProfile : Profile
    {
        public InternationalLicenseProfile()
        {
            CreateMap<InternationalLicense, InternationalLicenseDTO>()
                .ReverseMap();
        }
    }

}
