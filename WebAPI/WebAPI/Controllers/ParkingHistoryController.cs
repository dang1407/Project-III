using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ParkingHistoryController : BaseController<ParkingHistoryDTO, ParkingHistoryCreateDTO, ParkingHistoryUpdateDTO>
    {
        private readonly IParkingHistoryService _parkingHistoryService;
        private readonly IParkMemberService _parkMemberService;
        private readonly ICloudinaryService _cloudinaryService; 
        public ParkingHistoryController(IParkingHistoryService parkingHistoryService, IParkMemberService parkMemberService, ICloudinaryService cloudinaryService) : base(parkingHistoryService)
        {
            _parkingHistoryService = parkingHistoryService;
            _parkMemberService = parkMemberService;
            _cloudinaryService = cloudinaryService; 
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetParkedHistoryAsync(string? time, string? licensePlate, string? parkMemberCode)
        {
            if (!string.IsNullOrEmpty(time))
            {
                string[] times = time.Split('/');
                int length = times.Length;
                string date, month, year;
                year = times[length - 1];
                if (length == 3)
                {
                    month = times[length - 2];
                    date = times[length - 3];
                }
                else if (length == 2)
                {
                    date = "";
                    month = times[length - 2];
                }
                else
                {
                    date = "";
                    month = "";
                }

                var resultDate = await _parkingHistoryService.FindParkingHistoryByVehicleOutDateAsync(date, month, year);
                return Ok(resultDate);
            }

            return Ok();
        }

        [HttpGet]
        [Route("parking")]
        public async Task<IActionResult> GetParkingVehicleAsync(string? licensePlate, string? parkSlotCode)
        {
            //Console.WriteLine(licensePlate);
            if(!string.IsNullOrEmpty(licensePlate)) 
            {
                var result = await _parkingHistoryService.FindParkingVehicleAsync(licensePlate, null);
                var parkMember = await _parkMemberService.GetParkMemberByParkMemberCodeAsync(result[0].ParkMemberCode);
                return Ok(new
                {
                    result,
                    parkMember
                });
            }
            else
            {
                var parkKingHistory = await _parkingHistoryService.FindParkingVehicleAsync(null, parkSlotCode);
                return Ok(parkKingHistory);
            }
        }

        [HttpPost]
        [Route("enterVehicle")]
        public async Task<IActionResult> EnterVehicleToGarageAsync([FromBody] ParkingHistoryCreateDTO parkingHistoryCreateDTO)
        {
            var result = await _parkingHistoryService.EnterVehicleToGarageAsync(parkingHistoryCreateDTO);
            return Ok(result);
        }

        [HttpPut]
        [Route("enterVehicleOut")]
        public async Task<IActionResult> EnterVehicleOutGarageAsync( [FromBody] ParkingHistoryCreateDTO parkingHistoryCreateDTO)
        {
            var result = await _parkingHistoryService.EnterVehicleOutGarageAsync( parkingHistoryCreateDTO);
            return Ok(result);
        }

        [HttpPost]
        [Route("enterBikecycleToGarage")]
        public async Task<IActionResult> EnterBikecycleTpGarageAsync([FromForm] ParkingHistoryCreateDTO parkingHistoryCreateDTO)
        {
            if (parkingHistoryCreateDTO.VehicleInImage != null)
            {
                string? vehicleImageLink = _cloudinaryService.UpLoadImageToCloudinaryAsync(parkingHistoryCreateDTO.VehicleInImage, "Garage");
                if (!string.IsNullOrEmpty(vehicleImageLink))
                {
                    parkingHistoryCreateDTO.VehicleInImageLink = vehicleImageLink;
                    var result = await _parkingHistoryService.EnterVehicleToGarageAsync(parkingHistoryCreateDTO);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Không thể lưu ảnh vào cơ sở dữ liệu.");
                }
            }
            else return BadRequest("Thiếu thông tin ảnh.");
        }
    }
}
