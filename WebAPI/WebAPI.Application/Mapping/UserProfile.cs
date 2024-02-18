using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, LoginDTO>();
            CreateMap<LoginDTO, User>();
            CreateMap<RegisterDTO, User>();
            CreateMap<ForgotPasswordDTO, User>();
        }    
    }
}
