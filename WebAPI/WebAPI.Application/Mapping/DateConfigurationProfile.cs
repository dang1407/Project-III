using AutoMapper;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class DateConfigurationProfile : Profile
    {
        public DateConfigurationProfile() {
            CreateMap<DateConfigurationDTO, DateConfiguration>();
            CreateMap<DateConfiguration, DateConfigurationDTO>();
        }  
    }
}
