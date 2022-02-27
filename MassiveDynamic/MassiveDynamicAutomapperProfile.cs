using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MassiveDynamic.DTOs;
using MassiveDynamic.Data.Models;

namespace MassiveDynamic
{
    public class MassiveDynamicAutomapperProfile : Profile
    {
        public MassiveDynamicAutomapperProfile()
        {
            CreateMap<IdentityRole, IdentityRoleDto>().ReverseMap();
            CreateMap<ApplicationUser, GetApplicationUserDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<ContactPerson, ContactPersonDto>().ReverseMap();
            CreateMap<Document, GetDocumentDto>().ReverseMap();
        }
    }
}
