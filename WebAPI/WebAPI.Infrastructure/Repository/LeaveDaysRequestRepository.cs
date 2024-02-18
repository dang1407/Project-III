using Dapper;
using WebAPI.Application;
using WebAPI.Application.DTO;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace WebAPI.Infrastructure
{
    public class LeaveDaysRequestRepository : BaseRepository<LeaveDaysRequest>, ILeaveDaysRequestRepository
    {
        public LeaveDaysRequestRepository(IUnitOfWork uow) : base(uow) { }


        /// <summary>
        /// Hàm lấy thông tin đơn nghỉ theo phân trang
        /// </summary>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trong trang</param>
        /// <returns>Thông tin đơn nghỉ theo đúng trang</returns>
        /// Created By: nkdang 31/10/2023
        public async Task<List<LeaveDaysRequest>> GetLeaveDaysRequestsAsync(int page, int pageSize)
        {
            string sql = $"CALL Proc_Read_GetLeaveDaysRequestPagination(@page, @pageSize)";
            var param = new DynamicParameters(sql);
            param.Add("page", page - 1);
            param.Add("pageSize", pageSize);
            var result = await Uow.Connection.QueryAsync<LeaveDaysRequest>(sql, param, transaction: Uow.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// Hàm lấy ra số đơn xin nghỉ hiện có trong database
        /// </summary>
        /// <returns>Số đơn xin nghỉ hiện có trong database</returns>
        /// Created by: nkdang 1/11/2023
        public async Task<int> GetNumLeaveDaysRequestAsync()
        {
            string sql = "SELECT COUNT(LeaveDaysRequestId) FROM LeaveDaysRequest";
            var result = await Uow.Connection.QuerySingleOrDefaultAsync<int>(sql, transaction: Uow.Transaction);    
            return result;  
        }


        /// <summary>
        /// Hàm thêm mới một đơn nghỉ
        /// </summary>
        /// <param name="entity">Thông tin đơn nghỉ</param>
        /// <returns>Thông tin đơn nghỉ đã tạo thành công</returns>
        /// Created By: nkdang 31/10/2023
        public override async Task<LeaveDaysRequest> InsertAsync(LeaveDaysRequest entity)
        {
            string sql = $"CALL Proc_Create_InsertLeaveDaysRequest(@LeaveDaysRequestId, @EmployeeId, @FromDate, @ToDate, @SalaryRate, @ApproveBy, @SubtitueBy, @Type, @Note, @CreatedBy, @CreateDate, @ModifiedBy, @ModifiedDate)";
            var result = await Uow.Connection.QuerySingleAsync<LeaveDaysRequest>(sql, entity, transaction: Uow.Transaction);
            return entity;
        }

        /// <summary>
        /// Hàm sửa thông tin đơn nghỉ
        /// </summary>
        /// <param name="entity">Thông tin đơn nghỉ mới cần update thành</param>
        /// <returns>Thông tin đơn nghỉ mới đã được thay đổi</returns>
        /// Created By: nkdang 31/10/2023
        public override async Task<LeaveDaysRequest> UpdateAsync(LeaveDaysRequest entity)
        {
            string sql = $"CALL Proc_Create_UpdateLeaveDaysRequest(@LeaveDaysRequestId, @EmployeeId, @FromDate, @ToDate, @SalaryRate, @ApproveBy, @SubtitueBy, @Type, @Note, @CreatedBy, @CreateDate, @ModifiedBy, @ModifiedDate)";
            var result = await Uow.Connection.QuerySingleAsync<LeaveDaysRequest>(sql, entity, transaction: Uow.Transaction);
            return entity;
        }
    }
}
