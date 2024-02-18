using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebAPI.Application;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    //[Authorize(Roles = "admin")]
    public class ParkMembersController : BaseReadOnlyController<ParkMemberDTO>
    {
        private IParkMemberService _parkMemberService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        public ParkMembersController(IParkMemberService parkMemberService, ICloudinaryService cloudinaryService, IMapper mapper) : base(parkMemberService)
        {
            _parkMemberService = parkMemberService;
            _cloudinaryService = cloudinaryService; 
        }

        /// <summary>
        /// Hàm lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trong trang</param>
        /// <returns>Thông tin các nhân viên trong trang</returns>
        /// Created by: nkmdang (21/09/2023)
        [HttpGet]
        [Route("")]
        public async Task<dynamic> GetParkMembersFilterAsync(int page, int pageSize, string? parkMemberProperty)
        {
            if (!string.IsNullOrEmpty(parkMemberProperty))
            {
                // Kết quả cuối cùng
                var result = new List<ParkMemberDTO>();

                // Tìm theo mã khách hàng hội viên
                var parkMemberGetByParkMemberCode = await _parkMemberService.GetParkMemberByParkMemberCodeAsync(parkMemberProperty);
                if (parkMemberGetByParkMemberCode != null)
                {
                    result.Add(parkMemberGetByParkMemberCode);
                }

                // Tìm theo tên
                var parkMembersGetByFullName = await _parkMemberService.GetParkMemberByFullNameAsync(page, pageSize, parkMemberProperty);
                if (parkMembersGetByFullName != null)
                {
                    result.AddRange(parkMembersGetByFullName);
                }

                // Tìm theo số điện thoại
                var parkMemberGetByMobile = await _parkMemberService.GetParkMemberByMobileAsync(parkMemberProperty);
                if (parkMemberGetByMobile != null)
                {
                    result.Add(parkMemberGetByMobile);
                }

                // Tìm theo biển số xe
                var parkMemberGetByLicensePlate = await _parkMemberService.GetParkMemberByLicensePlateAsync(parkMemberProperty);
                if(parkMemberGetByLicensePlate != null)
                {
                    result.Add(parkMemberGetByLicensePlate);
                }
                return new
                {
                    data = result,
                    countParkMembers = result.Count()
                };
            }
            else
            {
                // Truy vấn theo phân trang
                var result = await _parkMemberService.GetParkMembersByPaginationAsync(page, pageSize);

                // Lấy ra số bản ghi trong CSDL
                var countParkMembers = await _parkMemberService.GetNumParkMembersAsync();
                return new
                {
                    data = result,
                    countParkMembers
                };
            }

        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> InsertParkMemberAsync([FromForm] ParkMemberCreateDTO parkMemberCreateDTO)
        {
            if(parkMemberCreateDTO.AvatarFile != null)
            {
                var imageUrl =  _cloudinaryService.UpLoadImageToCloudinaryAsync(parkMemberCreateDTO.AvatarFile, "Garage");

                if(string.IsNullOrEmpty(imageUrl)) 
                {
                    return BadRequest("Cannot upload image to Cloudinary.");
                }
                else
                {
                    parkMemberCreateDTO.AvatarLink = imageUrl;  
                }
            } 
            var result = await _parkMemberService.InsertAsync(parkMemberCreateDTO);
            return StatusCode(201, result);  
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateParkMemberAsync(Guid id, [FromForm] ParkMemberUpdateDTO parkMemberUpdateDTO)
        {
            if(parkMemberUpdateDTO.AvatarFile != null) 
            {
                var existParkMember = await _parkMemberService.GetByIdAsync(id);
                // Nếu như khách hàng đã có ảnh đại diện thì phải xóa ảnh cũ trên cloud đi rồi upload ảnh mới lên
                if(existParkMember != null ) 
                {
                    if (!string.IsNullOrEmpty(existParkMember.AvatarLink))
                    {
                    _cloudinaryService.DeleteImageInCloudinaryAsync(existParkMember.AvatarLink);
                    }
                } 
                else
                {
                    return NotFound("Không tìm thấy khách hàng");
                }
                var imageUrl = _cloudinaryService.UpLoadImageToCloudinaryAsync(parkMemberUpdateDTO.AvatarFile, "Garage");

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return BadRequest("Cannot upload image to Cloudinary.");
                }
                else
                {
                    parkMemberUpdateDTO.AvatarLink = imageUrl;
                }
            }

            var result = await _parkMemberService.UpdateAsync(id, parkMemberUpdateDTO);
            return Ok(result);
        }

        [HttpGet]
        [Route("ParkMemberCode")]
        public async Task<IActionResult> GetParkMemberByParkMemberCodeAsync(string parkMemberCode)
        {
            var result = await _parkMemberService.GetParkMemberByParkMemberCodeAsync(parkMemberCode);
            return Ok(result);
        }

        /// <summary>
        /// Hàm lấy mã khách hàng mới bằng mã khách hàng lớn nhất + 1
        /// </summary>
        /// <returns>Mã khách hàng gửi xe mới</returns>
        [HttpGet]
        [Route("NewParkMemberCode")]
        public async Task<IActionResult> GetNewParkMemberCodeAsync()
        {
            var result = await _parkMemberService.GetNewParkMemberCodeAsync();  
            return Ok(result);
        }

        [HttpDelete]
        [Route("{parkMemberId}")]
        public async Task<IActionResult> DeleteOneParkMemberByIdAsync(Guid parkMemberId)
        {
            var existParkMember = await _parkMemberService.GetByIdAsync(parkMemberId);  
            if(existParkMember != null) 
            {
                _cloudinaryService.DeleteImageInCloudinaryAsync(existParkMember.AvatarLink);
                await _parkMemberService.DeleteAsync(parkMemberId);
                return Ok();      
            } 
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Hàm truy vấn thông tin khách hàng gửi xe theo phân trang và tìm kiếm
        /// </summary>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trong trang</param>
        /// <param name="parkMemberProperty">Thuộc tính của khách hàng muốn tìm kiếm</param>
        /// <returns>Thông tin khách hàng</returns>
        /// Created by: nkmdang 15/1/2024
        [HttpGet]
        [Route("ParkMembersExcel")]
        public async Task<IActionResult> ExportParkMemberExcelAsync(int page, int pageSize, string? parkMemberProperty)
        {
            if (!string.IsNullOrEmpty(parkMemberProperty))
            {
                // Kết quả cuối cùng
                var result = new List<ParkMemberDTO>();

                // Tìm theo mã khách hàng hội viên
                var parkMemberGetByParkMemberCode = await _parkMemberService.GetParkMemberByParkMemberCodeAsync(parkMemberProperty);
                if (parkMemberGetByParkMemberCode != null)
                {
                    result.Add(parkMemberGetByParkMemberCode);
                }

                // Tìm theo tên
                var parkMembersGetByFullName = await _parkMemberService.GetParkMemberByFullNameAsync(page, pageSize, parkMemberProperty);
                if (parkMembersGetByFullName != null)
                {
                    result.AddRange(parkMembersGetByFullName);
                }

                // Tìm theo số điện thoại
                var parkMemberGetByMobile = await _parkMemberService.GetParkMemberByMobileAsync(parkMemberProperty);
                if (parkMemberGetByMobile != null)
                {
                    result.Add(parkMemberGetByMobile);
                }

                // Tìm theo biển số xe
                var parkMemberGetByLicensePlate = await _parkMemberService.GetParkMemberByLicensePlateAsync(parkMemberProperty);
                if (parkMemberGetByLicensePlate != null)
                {
                    result.Add(parkMemberGetByLicensePlate);
                }
                var excelBytes = await _parkMemberService.ExportParkMemberExcelAsync(result, page, pageSize);
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_khach_hang_gui_xe.xlsx");
            }
            else
            {
                // Truy vấn theo phân trang
                var result = await _parkMemberService.GetParkMembersByPaginationAsync(page, pageSize);

                var excelBytes = await _parkMemberService.ExportParkMemberExcelAsync(result, page, pageSize);

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_khach_hang_gui_xe.xlsx");
            }

        }
    }
}
