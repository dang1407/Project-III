using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;

namespace WebAPI.Controllers.Base
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseReadOnlyController<TDTO> : ControllerBase
    {
        protected readonly IBaseReadOnlyService<TDTO> BaseReadOnlyService;

        public BaseReadOnlyController(IBaseReadOnlyService<TDTO> baseReadOnlyService)
        {
            BaseReadOnlyService = baseReadOnlyService;
        }

        ///// <summary>
        ///// Lấy ra toàn bộ dữ liệu về tài nguyên 
        ///// </summary>
        ///// <returns>EmployeeEntity nếu thành công, mã lỗi nếu thất bại</returns>
        ///// Created by: nkmdang (20/09/2023)
        //[HttpGet]
        //[Route("")]
        //public async Task<List<TDTO>> GetFilterAsync(int page, int pageSize, string? properties)
        //{
        //    var result = await BaseReadOnlyService.GetFilterAsync(page, pageSize, properties);
        //    return result;
        //}

        /// <summary>
        /// Lấy ra thông tin tài nguyên theo id (char(36))
        /// </summary>
        /// <param name="id">Tham số nhận vào từ route</param>
        /// <returns>Thông tin tài nguyên nếu thành công, mã lỗi nếu thất bại</returns>
        /// Created by: nkmdang (20/09/2023)
        [HttpGet]
        [Route("{id}")]
        public async Task<TDTO> GetByIdAsync(Guid id)
        {
            var result = await BaseReadOnlyService.GetByIdAsync(id);
            return result;

        }
    }
}
