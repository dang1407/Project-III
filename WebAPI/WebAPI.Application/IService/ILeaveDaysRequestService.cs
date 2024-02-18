using WebAPI.Application.DTO;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface ILeaveDaysRequestService : IBaseService<LeaveDaysRequestDTO, LeaveDaysRequestCreateDTO, LeaveDaysRequestUpdateDTO>
    {
        /// <summary>
        /// Hàm lấy thông tin đơn nghỉ theo phân trang
        /// </summary>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trong trang</param>
        /// <returns>Thông tin đơn xin nghỉ</returns>
        /// Created By: nkdang 31/10/2023
        Task<List<LeaveDaysRequestDTO>> GetLeaveDaysRequestsAsync(int page, int pageSize);

        /// <summary>
        /// Hàm lấy ra số đơn xin nghỉ hiện có trong database
        /// </summary>
        /// <returns>Số đơn xin nghỉ hiện có trong database</returns>
        /// Created by: nkdang 1/11/2023
        Task<int> GetNumLeaveDaysRequestAsync();
    }
}
