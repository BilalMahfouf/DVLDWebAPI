using AutoMapper;
using Core.DTOs.License;
using DataAccessLayer;

namespace BusinessLoginLayer.Profiles
{
    public class InternationalLicenseProfile : Profile
    {
        public InternationalLicenseProfile()
        {
            CreateMap<InternationalLicenseDTO, InternationalLicense>()
                .ForMember(i => i.InternationalLicenseID, opt => opt.Ignore())
                .ForMember(i => i.IssueDate, opt => opt.Ignore())
                .ForMember(i => i.ExpirationDate, opt => opt.Ignore())
                .ForMember(i => i.IsActive, opt => opt.Ignore());

            CreateMap<InternationalLicense, ReadInternationalLicenseDTO>();



        }
    }

}
