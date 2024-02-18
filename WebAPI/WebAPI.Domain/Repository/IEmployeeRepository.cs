using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    /// <summary>
    /// Interface tương tác với Repository của Employee
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>  
    {
        
        /// <summary>
        /// Hàm lấy thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="page">Số thứ tự trang, bắt đầu từ 1</param>
        /// <param name="pageSize">Số bản ghi mỗi trang</param>
        /// <returns>Thông tin nhân viên theo trang</returns>
        /// Created by: nkmdang (18/09/2023)
        Task<List<Employee>> GetEmployeesPaginationAsync(int page, int pageSize);

        /// <summary>
        /// Hàm lấy thông tin nhân viên theo mã nhân viên (EmployeeCode)
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Thông tin nhân viên, mã lỗi khi không tìm thấy</returns>
        /// Created by: nkmdang (21/09/2023)
        //Task<Employee> GetEmployeeByCodeAsync(string employeeCode);    

        /// <summary>
        /// Hàm kiểm tra nhân viên tồn tại bằng Mã nhân viên (EmployeeCode)
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên (string)</param>
        /// <returns>Thông tin nhân viên nếu tìm thấy, null nếu không tìm thấy</returns>
        /// Created by: nkmdang (18/09/2023)
        Task<dynamic> IsExistEmployeeAsync(string employeeCode);

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

        Task<List<Employee>> GetByListIdAsync(List<Guid> ids);

        #region Chức năng tìm kiếm nhân viên
        /// <summary>
        /// Hàm tìm kiếm nhân viên theo số điện thoại
        /// </summary>
        /// <param name="mobile">Số điện thoại nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (2/10/2023)
        Task<Employee> GetEmployeeByMobileAsync(int page, int pageSize, string mobile);

        /// <summary>
        /// Hàm tìm kiếm nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<Employee> GetEmployeeByEmployeeCodeAsync(string employeeCode);

        /// <summary>
        /// Hàm tìm kiếm nhân viên theo họ tên đầy đủ nhân viên
        /// </summary>
        /// <param name="employeeFullName">Họ tên đầy đủ nhân viên</param>
        /// <returns>Thông tin nhân viên tìm được</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<List<Employee>> GetEmployeeByFullNameAsync(int page, int pageSize, string employeeFullName);
        #endregion

        #region Chức năng xuất file excel
        /// <summary>
        /// Hàm xuất file excel nhân viên
        /// </summary>
        /// <param name="employees">Thông tin nhân viên</param>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>File Excel dạng các byte</returns>
        /// Created by: nkmdang 08/10/2023
        Task<byte[]> ExportEmployeeExcelAsync(List<Employee> employees, int page, int pageSize);
        #endregion
    }
}
