using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigController : BaseReadOnlyController<ConfigDTO>
    {
        private readonly IConfigService _configService; 
        public ConfigController(IConfigService configService) : base(configService)
        {
            _configService = configService; 
        }


        /// <summary>
        /// Hàm lấy ra thông tin Config, trả về Config đầu tiên do CSDL chỉ thiết kế có 1 bản ghi
        /// </summary>
        /// <returns>Thông tin Config</returns>
        /// Created By: nkmdang (07/10/2023)
        [HttpGet]
        public async Task<ConfigDTO> GetConfigAsync()
        {
            var configs = await BaseReadOnlyService.GetFilterAsync(1, 1, null);   
            var config = configs.FirstOrDefault();
            return config;
        }

        [HttpPut]
        [Route("{id}")]
        public async  Task<ConfigDTO> UpdateConfigAsync(Guid id, [FromBody] ConfigUpdateDTO configUpdateDTO)
        {
            var result = await _configService.UpdateAsync(id, configUpdateDTO); 
            return result;
        }
    }
}
