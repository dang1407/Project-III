using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface IParkMemberRepository : IBaseRepository<ParkMember>
    {
        /// <summary>
        /// Hàm lấy thông tin khách hàng gửi xe theo phân trang
        /// </summary>
        /// <param name="page">Số thứ tự trang, bắt đầu từ 1</param>
        /// <param name="pageSize">Số bản ghi mỗi trang</param>
        /// <returns>Thông tin khách hàng gửi xe theo trang</returns>
        /// Created by: nkmdang (18/09/2023)
        Task<List<ParkMember>> GetParkMembersPaginationAsync(int page, int pageSize);  

        /// <summary>
        /// Hàm kiểm tra khách hàng gửi xe tồn tại bằng Mã khách hàng gửi xe (ParkMemberCode)
        /// </summary>
        /// <param name="parkMemberCode">Mã khách hàng gửi xe (string)</param>
        /// <returns>Thông tin khách hàng gửi xe nếu tìm thấy, null nếu không tìm thấy</returns>
        /// Created by: nkmdang (18/09/2023)
        Task<dynamic> IsExistParkMemberAsync(string parkMemberCode);

        /// <summary>
        /// Hàm lấy ra số khách hàng gửi xe trong database
        /// </summary>
        /// <returns>Số khách hàng gửi xe hiện có trong database</returns>
        /// Created by: nkmdang (26/09/2023)
        Task<int> GetNumParkMembersAsync();


        /// <summary>
        /// Hàm lấy mã khách hàng gửi xe mới
        /// </summary>
        /// <returns>Mã khách hàng gửi xe mới (string)</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<string> GetNewParkMemberCodeAsync();


        #region Chức năng tìm kiếm khách hàng gửi xe
        /// <summary>
        /// Hàm tìm kiếm khách hàng gửi xe theo số điện thoại
        /// </summary>
        /// <param name="mobile">Số điện thoại khách hàng gửi xe</param>
        /// <returns>Thông tin khách hàng gửi xe tìm được</returns>
        /// Created by: nkmdang (2/10/2023)
        Task<ParkMember?> GetParkMemberByMobileAsync(string mobile);

        /// <summary>
        /// Hàm tìm kiếm khách hàng gửi xe theo mã khách hàng gửi xe
        /// </summary>
        /// <param name="parkMemberCode">Mã khách hàng gửi xe</param>
        /// <returns>Thông tin khách hàng gửi xe tìm được</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<ParkMember?> GetParkMemberByParkMemberCodeAsync(string parkMemberCode);

        /// <summary>
        /// Hàm tìm kiếm khách hàng gửi xe theo biển số xe
        /// </summary>
        /// <param name="licensePlate">Biển số xe cần tìm kiếm</param>
        /// <returns></returns>
        /// Created by: nkmdang 17/1/2023
        Task<ParkMember?> GetParkMemberByLicensePlateAsync(string licensePlate);

        /// <summary>
        /// Hàm tìm kiếm khách hàng gửi xe theo họ tên đầy đủ khách hàng gửi xe
        /// </summary>
        /// <param name="parkMemberFullName">Họ tên đầy đủ khách hàng gửi xe</param>
        /// <returns>Thông tin khách hàng gửi xe tìm được</returns>
        /// Created by: nkmdang (27/09/2023)
        Task<List<ParkMember>?> GetParkMemberByFullNameAsync(int page, int pageSize, string parkMemberFullName);
        #endregion

        #region Chức năng xuất file excel
        /// <summary>
        /// Hàm xuất file excel khách hàng gửi xe
        /// </summary>
        /// <param name="parkMembers">Thông tin khách hàng gửi xe</param>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>File Excel dạng các byte</returns>
        /// Created by: nkmdang 08/10/2023
        Task<byte[]> ExportParkMembersExcelAsync(List<ParkMember> parkMembers, int page, int pageSize);
        #endregion
    }
}
