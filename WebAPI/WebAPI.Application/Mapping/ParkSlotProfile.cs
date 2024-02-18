using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkSlotProfile : Profile
    {
        public ParkSlotProfile() 
        {
            CreateMap<ParkSlot, ParkSlotDTO>();    
            CreateMap<ParkSlotCreateDTO, ParkSlot>();
            CreateMap<ParkSlotUpdateDTO, ParkSlot>();
            CreateMap<ParkSlotDTO, ParkSlot>(); 
        }    
    }
}
