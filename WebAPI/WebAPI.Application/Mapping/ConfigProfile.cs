using AutoMapper;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<Config, ConfigDTO>();
            CreateMap<ConfigDTO, Config>();
            CreateMap<ConfigCreateDTO, Config>();
            CreateMap<ConfigUpdateDTO, Config>();
        }

    }
}
