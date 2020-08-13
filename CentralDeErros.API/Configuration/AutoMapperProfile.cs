using CentralDeErros.Model.Models;
using CentralDeErros.Transport;
using Microsoft.AspNetCore.Identity;

namespace CentralDeErros.API
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        
        public AutoMapperProfile()
        {
            
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Model.Models.Environment, EnvironmentDTO>().ReverseMap();
            CreateMap<Level, LevelDTO>().ReverseMap();
            CreateMap<Microsservice, MicrosserviceDTO>().ReverseMap();
            CreateMap<Error, ErrorEntryDTO>().ReverseMap();
            CreateMap<Error, ErrorDTO>().ReverseMap();
            CreateMap<IdentityUser, RegisterUserDTO>().ReverseMap();
            CreateMap<IdentityUser, LoginDTO>().ReverseMap();
        }

    }
}
