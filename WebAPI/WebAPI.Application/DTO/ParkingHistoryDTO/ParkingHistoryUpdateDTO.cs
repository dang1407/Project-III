using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class ParkingHistoryUpdateDTO : BaseDTO
    {
        public Guid ParkingHistoryId { get; set; }
        public string? ParkMemberCode { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? LicensePlate { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày xe ra khỏi bãi không được để trống.")]
        [DataType(DataType.DateTime, ErrorMessage = "Ngày xe ra khỏi bãi phải có định dạng DateTime.")]
        public DateTimeOffset? VehicleOutDate { get; set; }
        [Required(ErrorMessage = "Mã vị trí gửi xe không được để trống")]
        public string? ParkSlotCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trạng thái của vị trí bãi gửi xe không được để trống.")]
        [RegularExpression(@"^[0-1]{1}$", ErrorMessage = "Trạng thái vị trí bãi gửi xe chỉ có thể là 0 hoặc 1.")]
        public int ParkSlotState { get; set; }

        [Required(ErrorMessage = "Tầng bãi gửi xe không được để trống.")]
        public string Floor { get; set; } = string.Empty;
        public int Vehicle { get; set; }
        public string? VehicleInImageLink { get; set; }
    }
}
