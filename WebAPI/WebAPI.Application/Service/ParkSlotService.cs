using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkSlotService : BaseService<ParkSlot, ParkSlotDTO, ParkSlotCreateDTO, ParkSlotUpdateDTO>, IParkSlotService
    {
        private readonly IMapper _mapper;
        private readonly IParkSlotRepository _parkSlotRepository;   
        public ParkSlotService(IParkSlotRepository parkSlotRepository, IMapper mapper) : base(parkSlotRepository)
        {
            _mapper = mapper;   
            _parkSlotRepository = parkSlotRepository;   
        }

        public async Task<ParkSlotDTO> GetParkSlotByLicensePlateAsync(string licensePlate)
        {
            var result = await _parkSlotRepository.GetParkSlotsByLicensePlateAsync(licensePlate);
            return MapEntityToDTO(result);
        }

        /// <summary>
        /// Hàm lấy thông tin bãi đỗ xe theo từng tầng
        /// </summary>
        /// <param name="floor">Tầng bãi đỗ xe cần xem trạng thái</param>
        /// <returns>Thông tin bãi đỗ xe theo tầng muốn xem (List<ParkSlotDTO>)</returns>
        public async Task<List<ParkSlotDTO>> GetParkSlotsByFloorAsync(string floor)
        {
            var parkSlots = await _parkSlotRepository.GetParkSlotsByFloorAsync(floor);
            var parkSlotDTOs = parkSlots.Select(parkSlot => MapEntityToDTO(parkSlot)).ToList();
            return parkSlotDTOs;
        }

        public override ParkSlot MapCreateDTOToEntity(ParkSlotCreateDTO createDTO)
        {
            var parkSlot = _mapper.Map<ParkSlot>(createDTO);
            return parkSlot;
        }

        public override ParkSlot MapDTOToEntity(ParkSlotDTO dto)
        {
            var parkSlot = _mapper.Map<ParkSlot>(dto);  
            return parkSlot;
        }

        public override ParkSlotDTO MapEntityToDTO(ParkSlot entity)
        {
            var parkSlotDto = _mapper.Map<ParkSlotDTO>(entity);
            return parkSlotDto;
        }

        public override ParkSlot MapUpdateDTOToEntity(ParkSlotUpdateDTO updateDTO, ParkSlot entity)
        {
            var parkSlot = _mapper.Map(updateDTO, entity);
            if(parkSlot.GetId() == Guid.Empty) 
            {
                parkSlot.SetId(entity.GetId()); 
            }
            return parkSlot;    
        }
    }
}
