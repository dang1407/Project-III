using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Domain
{
    /// <summary>
    /// Class validate nghiệp vụ cho dữ liệu của Employee
    /// </summary>
    public class EmployeeValidate : IEmployeeValidate
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentValidate _departmentValidate;
        public EmployeeValidate(IEmployeeRepository employeeRepository, IDepartmentValidate departmentValidate)
        {
            _employeeRepository = employeeRepository;
            _departmentValidate = departmentValidate;   
        }   


        

        /// <summary>
        /// Hàm kiểm tra nhân viên tồn tại theo Mã nhân viên (EmployeeCode)
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên (string)</param>
        /// <returns></returns>
        /// <exception cref="ConflictException"></exception>
        /// Created by: nkmdang (18/09/2023)
        public async Task CheckEmployeeExistAsync(Employee employee)
        {
            var isExistEmployee = await _employeeRepository.IsExistEmployeeAsync(employee.EmployeeCode); 
            if (isExistEmployee != false) 
            {
                throw new ConflictException(EmployeeMessageResource.EmployeeCodeExistMessage(employee.EmployeeCode), EmployeeMessageResource.EmployeeCodeExistMessage(employee.EmployeeCode), (int)EmployeeEnum.EmployeeCodeExistErrorCode);
            }
            
        }


        /// <summary>
        /// Hàm kiểm tra tên đơn vị có tồn tại hay không
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns>Mã lỗi nếu không thấy</returns>
        /// Created by: nkmdang (21/09/2023)
        public async Task CheckExistDepartmentByIdAsync(Employee employee)
        {
            await _departmentValidate.CheckExistDepartmentByIdAsync(employee.DepartmentId);
        }


        /// <summary>
        /// Hàm kiểm tra mã nhân viên mới nhập vào có trùng mã nhân viên của người khác không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên (string)</param>
        /// <param name="employeeId">Định danh nhân viên (Guid)</param>
        /// <returns>true nếu trùng và false nếu không trùng</returns>
        /// Created by: nkmdang (19/09/2023)
        public async Task CheckDuplicateEmployeeCodeAsync(string employeeCode, Guid employeeId)
        {
           var anotherEmployee = await _employeeRepository.GetEmployeeByEmployeeCodeAsync(employeeCode);
            if (anotherEmployee != null) 
            {
                Console.WriteLine(anotherEmployee.EmployeeId);
                Console.WriteLine(employeeId);
                if (anotherEmployee.EmployeeId != employeeId)
                {
                    throw new ConflictException(EmployeeMessageResource.EmployeeCodeExistMessage(employeeCode), EmployeeMessageResource.EmployeeCodeExistMessage(employeeCode), (int)EmployeeEnum.EmployeeCodeExistErrorCode);
                }
            }
        }
    }
}
