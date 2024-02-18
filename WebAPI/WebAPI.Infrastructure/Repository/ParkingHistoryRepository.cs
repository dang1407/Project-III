using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Infrastructure
{
    public class ParkingHistoryRepository : BaseRepository<ParkingHistory>, IParkingHistoryRepository
    {
        public ParkingHistoryRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<ParkingHistory>?> FindParkingHistoryByVehicleOutDateAsync(string? date, string? month, string year)
        {
            string sql = "SELECT * FROM ParkingHistory Where ";
            if(!string.IsNullOrEmpty(date))
            {
                sql += "DAY(VehicleOutDate) = @date AND ";
            }
            if(!string.IsNullOrEmpty(month)) 
            {
                sql += "MONTH(VehicleOutDate) = @month AND ";
            }
            sql += "YEAR(VehicleOutDate) = @year;";
            var param = new DynamicParameters();
            param.Add("date", date);
            param.Add("month", month);
            param.Add("year", year);
            var result = await Uow.Connection.QueryAsync<ParkingHistory>(sql, param);
            return result.ToList();
        }

        public Task<List<ParkingHistory>?> FindParkingHistoryByProperties(string propertyValues, string[] propertyNames)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ParkingHistory>> FindParkingVehicleAsync(string? licensePlate, string? parkSlotCode  )
        {
            string sql = "";
            // Nếu được gọi truy vấn qua biển số xe
            if(!string.IsNullOrEmpty (licensePlate) && string.IsNullOrEmpty(parkSlotCode))
            {
                sql = "SELECT ph.*, ps.ParkSlotState, ps.ParkSlotCode, ps.Floor FROM ParkingHistory ph, parkslot ps WHERE (ph.VehicleOutDate IS NULL OR ph.VehicleOutDate = '0001-01-01 00:00:00.000') AND ph.LicensePlate = ps.LicensePlate AND ph.LicensePlate = @LicensePlate;";
            }   
            // Nếu được gọi truy vấn qua 
            else if(!string.IsNullOrEmpty (parkSlotCode) && string.IsNullOrEmpty(licensePlate))
            {
                Console.WriteLine(parkSlotCode);
                sql = "SELECT ph.*, ps.ParkSlotState, ps.ParkSlotCode, ps.Floor, pm.FullName, pm.Address, pm.Mobile FROM ParkingHistory ph, parkslot ps, ParkMember pm WHERE (ph.VehicleOutDate IS NULL OR ph.VehicleOutDate = '0001-01-01 00:00:00.000') AND ph.CreatedDate = ps.CreatedDate AND ps.ParkSlotCode = @ParkSlotCode AND pm.ParkMemberCode = ph.ParkMemberCode;";
            }
            var param = new DynamicParameters();
            Console.WriteLine(sql);
            param.Add("LicensePlate", licensePlate);
            param.Add("ParkSlotCode", parkSlotCode);
            var result = await Uow.Connection.QueryAsync<ParkingHistory>(sql, param);
            return result.ToList();
        }

        public async Task<ParkingHistory> EnterVehicleToGarageAsync(ParkingHistory parkingHistory, ParkSlot parkSlot)
        {
            if(!string.IsNullOrEmpty(parkingHistory.LicensePlate))
            {
                string parkingHistorySQL = ParkingHistorySQL.CreateParkingHistorySQL();
                var parkingHistoryResult = await Uow.Connection.QueryAsync(parkingHistorySQL, parkingHistory, Uow.Transaction);
                string parkSlotSQL = "Update ParkSlot SET ParkSlotState = @ParkSlotState, LicensePlate = @LicensePlate, Vehicle = @Vehicle, VehicleInDate = @CreatedDate, CreatedDate = @CreatedDate,CreatedBy = @CreatedBy, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy WHERE ParkSlotCode = @ParkSlotCode;";
                Console.WriteLine(parkSlot.ToString());
                var param = new DynamicParameters(parkingHistory);
                param.Add("ParkSlotCode", parkSlot.ParkSlotCode);
                param.Add("ParkSlotState", 1);
                var parkSlotResult = await Uow.Connection.QuerySingleOrDefaultAsync(parkSlotSQL, param, Uow.Transaction);

                return parkSlotResult;
            }
            else
            {
                if (parkingHistory.Vehicle == 0)
                {
                    string parkingHistorySQL = ParkingHistorySQL.CreateParkingHistorySQL();
                    var parkingHistoryResult = await Uow.Connection.QueryAsync(parkingHistorySQL, parkingHistory, Uow.Transaction);
                    string parkSlotSQL = "Update ParkSlot SET ParkSlotState = @ParkSlotState, LicensePlate = @LicensePlate, Vehicle = @Vehicle, VehicleInDate = @CreatedDate, CreatedDate = @CreatedDate,CreatedBy = @CreatedBy, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy WHERE ParkSlotCode = @ParkSlotCode;";
                    Console.WriteLine(parkSlot.ToString());
                    var param = new DynamicParameters(parkingHistory);
                    param.Add("ParkSlotCode", parkSlot.ParkSlotCode);
                    param.Add("ParkSlotState", 1);
                    var parkSlotResult = await Uow.Connection.QuerySingleOrDefaultAsync(parkSlotSQL, param, Uow.Transaction);
                    return parkingHistory;
                }
                else throw new Exception("Phương tiện không phải xe đạp thì cần có biển số.");
            }
            
        }

        public async Task<ParkingHistory> EnterVehicleOutGarageAsync(ParkingHistory parkingHistory)
        {
            string parkingHistorySQL = ParkingHistorySQL.UpdateParkingHistorySQL();
            var parkingHistoryResult = await Uow.Connection.QueryAsync(parkingHistorySQL, parkingHistory, Uow.Transaction);
            string parkSlotSQL = "Update ParkSlot SET ParkSlotState = @ParkSlotState, LicensePlate = @LicensePlate, Vehicle = @Vehicle, VehicleInDate = @CreatedDate WHERE ParkSlotCode = @ParkSlotCode;";
            //Console.WriteLine(parkSlot);
            //var param = new DynamicParameters();
            //param.Add("ParkSlotCode", parkSlot.ParkSlotCode);
            //param.Add("ParkSlotState", 1);
            var parkSlotResult = await Uow.Connection.QuerySingleOrDefaultAsync(parkSlotSQL, parkingHistory, Uow.Transaction);

            return parkSlotResult;
            //throw new ConflictException("Không thể thêm thông tin của xe vào cơ sở dữ liệu.", 409);
        }
    }
}
