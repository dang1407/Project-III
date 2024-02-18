using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IConfigService : IBaseService<ConfigDTO, ConfigCreateDTO, ConfigUpdateDTO>
    {
        ///// <summary>
        ///// Hàm lấy ra định dạng ngày, tháng, năm đang được sử dụng
        ///// </summary>
        ///// <returns>Định dạng ngày tháng năm đang được sử dụng (dd/MM/yyyy hoặc định dạng khác)</returns>
        ///// Created by: nkmdang (07/10/2023)

        //Task UpdateConfigAsync();

        ///// <summary>
        ///// Hàm lấy ra thông tin Config
        ///// </summary>
        ///// <returns>Thông tin Config</returns>
        ///// Created By: nkmdang (07/10/2023)
        //Task<ConfigDTO> GetConfigAsync();
    }
}
