using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface IParkingHistoryRepository : IBaseRepository<ParkingHistory>
    {
        Task<List<ParkingHistory>?> FindParkingHistoryByVehicleOutDateAsync(string date, string month, string year);
        Task<List<ParkingHistory>?> FindParkingHistoryByProperties(string propertyValues, string[] propertyNames);

        Task<List<ParkingHistory>> FindParkingVehicleAsync(string? licensePlate, string? parkSlotCode);
        Task<ParkingHistory> EnterVehicleToGarageAsync(ParkingHistory parkingHistory, ParkSlot parkSlot);
        Task<ParkingHistory> EnterVehicleOutGarageAsync(ParkingHistory parkingHistory);

        
    }
}
