using WebAPI.Application;
using WebAPI;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Resources;
using WebAPI.Domain.Resource;

namespace WebAPI.Application
{
    public class EmployeeService : BaseService<Employee, EmployeeDTO, EmployeeCreateDTO, EmployeeUpdateDTO>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeValidate _employeeValidate;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeValidate employeeValidate, IMapper mapper) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeValidate = employeeValidate;   
            _mapper = mapper;
        }


        /// <summary>
        /// Hàm lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="page">Số thứ tự trang, bắt đầu từ 1</param>
        /// <param name="pageSize">Số bản ghi mỗi trang</param>
        /// <returns>Thông tin nhân viên theo trang</returns>
        /// Created by: nkmdang (18/09/2023)
        public async Task<List<EmployeeDTO>> GetEmployeesByPaginationAsync(int page, int pageSize)
        {
            var employees = await _employeeRepository.GetEmployeesPaginationAsync(page, pageSize);  

            var employeeDTOs = employees.Select(employee => MapEntityToDTO(employee)).ToList(); 
            return employeeDTOs;
        }


        

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới (string)</returns>
        /// Created by: nkmdang (27/09/2023)
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            var result = await _employeeRepository.GetNewEmployeeCodeAsync();   
            return result;  
        }


        /// <summary>
        /// Hàm Validate nghiệp vụ cho thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Thông tin Entity thêm mới</param>
        /// <returns>Ném ra lỗi nếu đầu vào không thỏa mãn nghiệp vụ</returns>
        /// Created by: nkmdang (19/09/2023)
        public async override Task ValidateCreateBusinessAsync(Employee entity)
        {

            // Mã nhân viên mới nhập chưa tồn tại
            await _employeeValidate.CheckEmployeeExistAsync(entity);

            // Kiểm tra id đơn vị hợp lệ
            await _employeeValidate.CheckExistDepartmentByIdAsync(entity);
        }

        /// <summary>
        /// Hàm Validate nghiệp vụ cho sửa đổi 1 bản ghi
        /// </summary>
        /// <param name="entity">Thông tin Entity sửa đổi</param>
        /// <returns>Ném ra lỗi nếu đầu vào không thỏa mãn nghiệp vụ</returns>
        /// Created by: nkmdang (19/09/2023)
        public async override Task ValidateUpdateBusinessAsync(Employee entity)
        {
            // Mã nhân viên sửa đổi có thể trùng mã nhân viên cũ, không trùng mã nhân viên người khác
            await _employeeValidate.CheckDuplicateEmployeeCodeAsync(entity.EmployeeCode, entity.EmployeeId);
        }

        /// <summary>
        /// Hàm chuyển đổi từ EmployeeCreateDTO sang Employee
        /// </summary>
        /// <param name="createDTO">EmployeeCreate </param>
        /// <returns>Employee</returns>
        /// Created by: nkmdang (21/09/2023)
        public override Employee MapCreateDTOToEntity(EmployeeCreateDTO createDTO)
        {
            var employee = _mapper.Map<Employee>(createDTO);
            // Kiểm tra EmployeeId chưa có 
            if(employee.EmployeeId == Guid.Empty)
            {
                employee.EmployeeId = Guid.NewGuid();   
            }
            return employee;
        }


        /// <summary>
        /// Hàm chuyển đổi EmployeeUpdateDTO sang Employee
        /// </summary>
        /// <param name="updateDTO">Đối tượng UpdateEmployeeDTO nhận vào body</param>
        /// <param name="entity"></param> Đối tượng Employee đầu đủ thông tin 
        /// <returns>Employee</returns>
        /// Created by: nkmdang (21/09/2023)
        public override Employee MapUpdateDTOToEntity(EmployeeUpdateDTO updateDTO, Employee entity)
        {
            var employee = _mapper.Map(updateDTO, entity);


            return employee;
        }


        /// <summary>
        /// Hàm chuyển đổi từ Employee sang DTO
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns></returns>
        /// Created by: nkmdang (21/09/2023)
        public override EmployeeDTO MapEntityToDTO(Employee employee)
        {
            var employeeDTO = _mapper.Map<EmployeeDTO> (employee);
            return employeeDTO;
        }

        /// <summary>
        /// Hàm lấy ra số nhân viên trong database
        /// </summary>
        /// <returns>Số nhân viên hiện có trong database</returns>
        /// Created by: nkmdang (26/09/2023)
        public async Task<int> GetNumEmployeesAsync()
        {
            int result = await _employeeRepository.GetNumEmployeesAsync();
            return result;
        }

        #region Chức năng tìm kiếm nhân viên
        /// <summary>
        /// Hàm tìm kiếm nhân viên theo số điện thoại
        /// </summary>
        /// <param name="mobile">Số điện thoại nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (2/10/2023)
        public async Task<EmployeeDTO> GetEmployeeByMobileAsync(int page, int pageSize, string mobile)
        {
            var employee = await _employeeRepository.GetEmployeeByMobileAsync(page, pageSize, mobile);
            var result = MapEntityToDTO(employee);
            return result;
        }

        /// <summary>
        /// Hàm tìm kiếm nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (27/09/2023)
        public async Task<EmployeeDTO> GetEmployeeByEmployeeCodeAsync(string employeeCode)
        {
            var employee = await _employeeRepository.GetEmployeeByEmployeeCodeAsync(employeeCode);
            var result = MapEntityToDTO(employee);
            return result;
        }


        /// <summary>
        /// Hàm tìm kiếm nhân viên theo họ tên đầy đủ nhân viên
        /// </summary>
        /// <param name="employeeFullName">Họ tên đầy đủ nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (27/09/2023)
        public async Task<List<EmployeeDTO>> GetEmployeeByFullNameAsync(int page, int pageSize, string employeeFullName)
        {
            var employees = await _employeeRepository.GetEmployeeByFullNameAsync(page, pageSize, employeeFullName);
            var result = employees.Select(employee => MapEntityToDTO(employee)).ToList();
            return result;
        }
        #endregion

        #region Chức năng xuất file excel
        /// <summary>
        /// Hàm xuất thông tin nhân viên ra excel
        /// </summary>
        /// <param name="employeeDTOs">Thông tin nhân viên</param>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trong trang</param>
        /// <returns></returns>
        /// Created by: nkmdang 08/10/2023
        public async Task<byte[]> ExportEmployeeExcelAsync(List<EmployeeDTO> employeeDTOs, int page, int pageSize)
        {
            // Chuyển đổi DTO sang Entity các nhân viên tìm được
            var employees = employeeDTOs.Select(employeeDTO => MapDTOToEntity(employeeDTO)).ToList();   

            // Gọi EmployeeRepository để lấy dữ liệu dạng các byte
            var excelBytes = await _employeeRepository.ExportEmployeeExcelAsync(employees, page, pageSize); 
            
            // Trả về bytes cho Controller
            return excelBytes;
        }

        /// <summary>
        /// Chuyển đổi từ DTO sang Entity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Employee MapDTOToEntity(EmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);
            return employee;
        }



        #endregion


        /// <summary>
        /// Hàm lấy ra thông tin nhiều nhân viên theo danh sách Id
        /// </summary>
        /// <param name="ids">Danh sách Id của nhân viên</param>
        /// <returns>Thông tin các nhân viên tìm thấy</returns>
        /// Created By: nkdang 31/10/2023
        public async Task<List<EmployeeDTO>> GetByListIdAsync(List<Guid> ids)
        {
            var employees = await _employeeRepository.GetByListIdAsync(ids);
            var employeeDTOS = employees.Select(employee => MapEntityToDTO(employee)).ToList();
            return employeeDTOS;
        }
    }
}

