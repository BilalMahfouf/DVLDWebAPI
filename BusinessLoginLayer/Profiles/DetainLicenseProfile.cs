using AutoMapper;
using BusinessLoginLayer.DTOs.Detain;
using DataAccessLayer;

namespace BusinessLoginLayer.Profiles
{
    public class DetainLicenseProfile : Profile
    {
        public DetainLicenseProfile()
        {
            CreateMap<DetainedLicense, DetainLicenseDTO>().ReverseMap();
            CreateMap<UpdateDetainedLicenseDTO, DetainedLicense>()
                .ForMember(d => d.DetainID, opt => opt.Ignore())
                .ForMember(d => d.FineFees, opt => opt.Ignore())
                .ForMember(d => d.DetainDate, opt => opt.Ignore())
                .ForMember(d => d.CreatedByUserID, opt => opt.Ignore())
                .ForMember(d => d.LicenseID, opt => opt.Ignore());
        }
    }   

}
