using CentralDeErros.Model.Models;
using CentralDeErros.Transport;
using CentralDeErros.Transport.MicrosserviceDTOs;
using Microsoft.AspNetCore.Identity;

namespace CentralDeErros.API
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        
        public AutoMapperProfile()
        {
            
            CreateMap<Model.Models.Environment, EnvironmentDTO>().ReverseMap();
            CreateMap<Level, LevelDTO>().ReverseMap();
            CreateMap<Microsservice, MicrosserviceDTO>().ReverseMap();
            CreateMap<Microsservice, MicrosserviceNameOnlyDTO>().ReverseMap();
            CreateMap<Microsservice, MicrosserviceClientIdOnlyDTO>().ReverseMap();
            CreateMap<Microsservice, MicrosserviceRegisterDTO>().ReverseMap();
            CreateMap<Microsservice, MicrosserviceLoginDTO>().ReverseMap();
            CreateMap<Error, ErrorEntryDTO>().ReverseMap();
            CreateMap<Error, ErrorDTO>().ReverseMap();
            CreateMap<IdentityUser, RegisterUserDTO>().ReverseMap();
            CreateMap<IdentityUser, LoginDTO>().ReverseMap();
            CreateMap<IdentityUser, UserGetDTO>().ReverseMap();
            CreateMap<IdentityUser, UserUpdateDTO>().ReverseMap();
            CreateMap<IdentityUser, UserUpdatePassword>().ReverseMap();
        }

    }
}