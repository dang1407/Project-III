using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IParkingHistoryService : IBaseService<ParkingHistoryDTO, ParkingHistoryCreateDTO, ParkingHistoryUpdateDTO>
    {
        Task<List<ParkingHistoryDTO>?> FindParkingHistoryByVehicleOutDateAsync(string date, string month, string year);
        Task<List<ParkingHistoryDTO>?> FindParkingHistoryByProperties(string propertyValues, string[] propertyNames);

        Task<List<ParkingHistoryDTO>> FindParkingVehicleAsync(string? licensePlate, string? parkSlotCode);

        Task<ParkingHistoryDTO> EnterVehicleToGarageAsync(ParkingHistoryCreateDTO parkingHistoryCreateDTO); 
        Task<ParkingHistoryDTO> EnterVehicleOutGarageAsync( ParkingHistoryCreateDTO parkingHistoryCreateDTO);
    }
}
