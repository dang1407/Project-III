using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkingHistoryService : BaseService<ParkingHistory, ParkingHistoryDTO, ParkingHistoryCreateDTO, ParkingHistoryUpdateDTO>, IParkingHistoryService
    {
        private readonly IParkingHistoryRepository _parkingHistoryRepository;
        private readonly IMapper _mapper;
        public ParkingHistoryService(IParkingHistoryRepository parkingHistoryRepository, IMapper mapper) : base(parkingHistoryRepository) 
        { 
            _mapper = mapper;   
            _parkingHistoryRepository = parkingHistoryRepository;      
        }

        public async Task<ParkingHistoryDTO> EnterVehicleOutGarageAsync(ParkingHistoryCreateDTO parkingHistoryCreateDTO)
        {
            var parkSlot = _mapper.Map<ParkSlot>(parkingHistoryCreateDTO);
            var parkingHistory = _mapper.Map<ParkingHistory>(parkingHistoryCreateDTO);
            var existVehicle = await FindParkingVehicleAsync(parkingHistory.LicensePlate, null);

            if (existVehicle.Count == 0)
            {
                throw new NotFoundException("Xe này hiện đang không có ở trong bãi đỗ xe. Vui lòng kiểm tra lại.", 404);
            }

            var result = await _parkingHistoryRepository.EnterVehicleOutGarageAsync(parkingHistory);
            return MapEntityToDTO(result);
        }


        /// <summary>
        /// Hàm thêm xe vào bãi đỗ xe
        /// </summary>
        /// <param name="parkingHistoryCreateDTO">Thông tin nhập xe vào bãi, bao gồm thông tin vị trí gửi xe để cập nhật trạng thái</param>
        /// <returns></returns>
        /// Created by: nkmdang 18/1/2024
        public async Task<ParkingHistoryDTO> EnterVehicleToGarageAsync( ParkingHistoryCreateDTO parkingHistoryCreateDTO)
        {
            parkingHistoryCreateDTO.CreatedDate ??= DateTime.Now;
            parkingHistoryCreateDTO.CreatedBy ??= "nkmdang";
            var parkSlot = _mapper.Map<ParkSlot>(parkingHistoryCreateDTO);
            var parkingHistory = _mapper.Map<ParkingHistory>(parkingHistoryCreateDTO);
            if (parkingHistory.GetId() == Guid.Empty)
            {
                parkingHistory.SetId(Guid.NewGuid());
            }

            var existVehicle = await FindParkingVehicleAsync(parkingHistory.LicensePlate, null);

            if (existVehicle.Count > 0 && existVehicle[0].Vehicle > 0) 
            {
                throw new ConflictException("Xe này hiện đang có ở trong bãi đỗ xe. Vui lòng kiểm tra lại.",409);
            }

            var result = await _parkingHistoryRepository.EnterVehicleToGarageAsync(parkingHistory, parkSlot);
            return MapEntityToDTO(result);   
        }

        public async Task<List<ParkingHistoryDTO>?> FindParkingHistoryByDateTimeAsync(string date, string month, string year)
        {
            var result = await _parkingHistoryRepository.FindParkingHistoryByVehicleOutDateAsync(date, month, year);
            return result.Select(parkingHistory => MapEntityToDTO(parkingHistory)).ToList();
        }

        public Task<List<ParkingHistoryDTO>?> FindParkingHistoryByProperties(string propertyValues, string[] propertyNames)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ParkingHistoryDTO>?> FindParkingHistoryByVehicleOutDateAsync(string date, string month, string year)
        {
            var result = await _parkingHistoryRepository.FindParkingHistoryByVehicleOutDateAsync(date, month, year);
            return result.Select(parkingHistory => MapEntityToDTO(parkingHistory)).ToList();
        }

        public async Task<List<ParkingHistoryDTO>> FindParkingVehicleAsync(string? licensePlate, string? parkSlotCode)
        {
            Console.WriteLine(licensePlate);
            if(!string.IsNullOrEmpty(licensePlate) && string.IsNullOrEmpty(parkSlotCode)) 
            { 
                var result = await _parkingHistoryRepository.FindParkingVehicleAsync(licensePlate, null);
                return result.Select(parkingHistory => MapEntityToDTO(parkingHistory)).ToList();
            }

            if (!string.IsNullOrEmpty(parkSlotCode) && string.IsNullOrEmpty(licensePlate))
            {
                var result = await _parkingHistoryRepository.FindParkingVehicleAsync(licensePlate, parkSlotCode);
                return result.Select(parkingHistory => MapEntityToDTO(parkingHistory)).ToList();
            }
            return [];
        }

        public override ParkingHistory MapCreateDTOToEntity(ParkingHistoryCreateDTO createDTO)
        {
            var result = _mapper.Map<ParkingHistory>(createDTO); 
            return result;
        }

        public override ParkingHistory MapDTOToEntity(ParkingHistoryDTO dto)
        {
            var parkingHistory = _mapper.Map<ParkingHistory>(dto);
            return parkingHistory;  
        }

        public override ParkingHistoryDTO MapEntityToDTO(ParkingHistory entity)
        {
            var parkingHistoryDTO = _mapper.Map<ParkingHistoryDTO>(entity);
            return parkingHistoryDTO;
        }

        public override ParkingHistory MapUpdateDTOToEntity(ParkingHistoryUpdateDTO updateDTO, ParkingHistory entity)
        {
            var parkingHistory = _mapper.Map(updateDTO, entity); 
            return parkingHistory;
        }
    }
}
