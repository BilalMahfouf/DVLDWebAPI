using AutoMapper;
using Core.DTOs.Detain;
using DataAccessLayer;

namespace BusinessLoginLayer.Profiles
{
    public class DetainLicenseProfile : Profile
    {
        public DetainLicenseProfile()
        {
            CreateMap<DetainedLicense, DetainLicenseDTO>().ReverseMap();
            CreateMap<UpdateDetainedLicenseDTO, DetainedLicense>()
                .ForMember(d => d.FineFees, opt => opt.Ignore())
                .ForMember(d => d.DetainDate, opt => opt.Ignore())
                .ForMember(d => d.CreatedByUserID, opt => opt.Ignore())
                .ForMember(d => d.LicenseID, opt => opt.Ignore())
                .ForMember(d => d.ReleaseDate, opt => opt.Ignore())
                .ForMember(d => d.IsReleased, opt => opt.Ignore());

            CreateMap<DetainedLicenses_View, DetainedLicenseDashboardDTO>();

        }

    }
}
