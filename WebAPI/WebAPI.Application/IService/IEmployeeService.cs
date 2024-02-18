using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IEmployeeService : IBaseService<EmployeeDTO, EmployeeCreateDTO, EmployeeUpdateDTO> 
    {
       

        /// <summary>
        /// Hàm lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="page">Số thứ tự trang, bắt đầu từ 1</param>
        /// <param name="pageSize">Số bản ghi mỗi trang</param>
        /// <returns>Thông tin nhân viên theo trang</returns>
        /// Created by: nkmdang (18/09/2023)
        Task<List<EmployeeDTO>> GetEmployeesByPaginationAsync(int page, int pageSize);


        /// <summary>
        /// Hàm lấy ra số nhân viên trong database
        /// </summary>
        /// <returns>Số nhân viên hiện có trong database</returns>
        /// Created by: nkmdang (26/09/2023)
        Task<int> GetNumEmployeesAsync();

        

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới (string)</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<string> GetNewEmployeeCodeAsync();

        Task<List<EmployeeDTO>> GetByListIdAsync(List<Guid> ids);

        #region Chức năng tìm kiếm nhân viên
        /// <summary>
        /// Hàm tìm kiếm nhân viên theo số điện thoại
        /// </summary>
        /// <param name="mobile">Số điện thoại nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (2/10/2023)
        Task<EmployeeDTO> GetEmployeeByMobileAsync(int page, int pageSize, string mobile);

        /// <summary>
        /// Hàm tìm kiếm nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<EmployeeDTO> GetEmployeeByEmployeeCodeAsync(string employeeCode);

        /// <summary>
        /// Hàm tìm kiếm nhân viên theo họ tên đầy đủ nhân viên
        /// </summary>
        /// <param name="employeeFullName">Họ tên đầy đủ nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<List<EmployeeDTO>> GetEmployeeByFullNameAsync(int page, int pageSize, string employeeFullName);
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
        Task<byte[]> ExportEmployeeExcelAsync(List<EmployeeDTO> employeeDTOs, int page, int pageSize);
        #endregion
    }
}
