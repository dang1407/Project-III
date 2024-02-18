using Dapper;
using Microsoft.AspNetCore.Mvc;
using static Dapper.SqlMapper;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using WebAPI.Domain;
using WebAPI.Application;
using OfficeOpenXml;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class EmployeesController : BaseController<EmployeeDTO, EmployeeCreateDTO, EmployeeUpdateDTO>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
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
        public async Task<dynamic> GetEmployeesFilterAsync(int page, int pageSize, string? employeeProperty)
        {
            if(!string.IsNullOrEmpty(employeeProperty)) 
            {
                // Kết quả cuối cùng
                var result = new List<EmployeeDTO>();

                // Tìm theo mã nhân viên
                var employeeGetByEmployeeCode = await _employeeService.GetEmployeeByEmployeeCodeAsync(employeeProperty);
                if(employeeGetByEmployeeCode != null)
                {
                    result.Add(employeeGetByEmployeeCode);
                }

                // Tìm theo tên
                var employeesGetByFullName = await _employeeService.GetEmployeeByFullNameAsync(page, pageSize, employeeProperty);
                if(employeesGetByFullName != null)
                {
                    result.AddRange(employeesGetByFullName);
                }

                // Tìm theo số điện thoại
                var employeeGetByMobile = await _employeeService.GetEmployeeByMobileAsync(page, pageSize, employeeProperty);
                if(employeeGetByMobile != null)
                {
                    result.Add(employeeGetByMobile);
                }

                return new  {
                    data = result,
                    countEmployees = result.Count()
                };
            }
            else
            {
                // Truy vấn theo phân trang
                var result = await _employeeService.GetEmployeesByPaginationAsync(page, pageSize);

                // Lấy ra số bản ghi trong CSDL
                var countEmployees = await _employeeService.GetNumEmployeesAsync();
                return new
                {
                    data = result,
                    countEmployees
                } ;   
            } 
                
        }

        /// <summary>
        /// Hàm lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <returns>Thông tin các nhân viên trong trang</returns>
        /// Created by: nkmdang (21/09/2023)
        [HttpGet]
        [Route("NumEmployee")]
        public async Task<int> GetNumEmployeeAsync()
        {
            var result = await _employeeService.GetNumEmployeesAsync();

            return result;
        }

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới (string)</returns>
        /// Created by: nkmdang (27/09/2023)
        [HttpGet]
        [Route("NewEmployeeCode")]
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            var result = await _employeeService.GetNewEmployeeCodeAsync();
            return result;
        }

        [HttpGet]
        [Route("EmployeesExcel")]
        public async Task<dynamic> ExportCurrentPageExcelAsync(int page, int pageSize, string? employeeProperty)
        {
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            // Gọi dữ liệu từ trang hiện tại đang xem
            if (string.IsNullOrEmpty(employeeProperty))
            {
                employees = await _employeeService.GetEmployeesByPaginationAsync(page, pageSize);
            }
            else
            {

                // Tìm theo mã nhân viên
                var employeeGetByEmployeeCode = await _employeeService.GetEmployeeByEmployeeCodeAsync(employeeProperty);
                if (employeeGetByEmployeeCode != null)
                {
                    employees.Add(employeeGetByEmployeeCode);
                }

                // Tìm theo tên
                var employeesGetByFullName = await _employeeService.GetEmployeeByFullNameAsync(page, pageSize, employeeProperty);
                if (employeesGetByFullName != null)
                {
                    employees.AddRange(employeesGetByFullName);
                }

                // Tìm theo số điện thoại
                var employeeGetByMobile = await _employeeService.GetEmployeeByMobileAsync(page, pageSize, employeeProperty);
                if (employeeGetByMobile != null)
                {
                    employees.Add(employeeGetByMobile);
                }
            }

            // Lấy ra dữ liệu excel dạng byte
            var excelBytes = await _employeeService.ExportEmployeeExcelAsync(employees, page, pageSize  );

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhan_vien.xlsx");
        }
    }
}
