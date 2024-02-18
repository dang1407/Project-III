using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ParkSlotsController : BaseController<ParkSlotDTO, ParkSlotCreateDTO, ParkSlotUpdateDTO>
    {
        private readonly IParkSlotService _parkSlotService; 
        public ParkSlotsController(IParkSlotService parkSlotService) : base(parkSlotService)
        {
            _parkSlotService = parkSlotService; 
        }


        /// <summary>
        /// Hàm lấy thông tin bãi đỗ xe theo từng tầng
        /// </summary>
        /// <param name="floor">Tầng bãi đỗ xe cần xem trạng thái</param>
        /// <returns>Thông tin bãi đỗ xe theo tầng muốn xem (List<ParkSlotDTO>)</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetParkSlotsByFloorAsync(string floor)
        {
            var result = await _parkSlotService.GetParkSlotsByFloorAsync(floor);
            return Ok(result);  
        }

        [HttpGet]
        [Route("parking")]
        public async Task<IActionResult> GetParkingVehicleInSlotAsync(string licensePlate)
        {
            var result = await _parkSlotService.GetParkSlotByLicensePlateAsync(licensePlate);   
            return Ok(result);  
        }
    }
}
