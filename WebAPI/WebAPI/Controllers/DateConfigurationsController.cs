using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    public class DateConfigurationsController : BaseReadOnlyController<DateConfigurationDTO>
    {
        public DateConfigurationsController(IDateConfigurationService dateFormatService) : base(dateFormatService)
        {
        }


        /// <summary>
        /// Hàm lấy ra tất cả thông tin DateConfigurationDTO
        /// </summary>
        /// <returns>Thông tin DateConfigurationDTO</returns>
        /// Created By: nkmdang (07/10/2023)
        [HttpGet]
        public async Task<List<DateConfigurationDTO>> GetFilterAsync()
        {
            var result = await BaseReadOnlyService.GetFilterAsync(1, 100, null);
            return result;
        }
    }
}
