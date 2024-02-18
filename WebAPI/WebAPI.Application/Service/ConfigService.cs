using AutoMapper;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class ConfigService : BaseService<Config, ConfigDTO, ConfigCreateDTO, ConfigUpdateDTO>, IConfigService
    {
        private readonly IConfigRepository _configRepository;
        private readonly IMapper _mapper;

        public ConfigService( IConfigRepository configRepository, IMapper mapper) : base(configRepository)
        {
            _configRepository = configRepository;
            _mapper = mapper;
        }



        ///// <summary>
        ///// Hàm lấy ra định dạng ngày, tháng, năm đang được sử dụng
        ///// </summary>
        ///// <returns>Định dạng ngày tháng năm đang được sử dụng (dd/MM/yyyy hoặc định dạng khác)</returns>
        ///// Created by: nkmdang (07/10/2023)
        //public async Task Upda()
        //{
        //    var result = await _configRepository.SetDateFormatStringAsync();
        //    return result;
        //}

        #region Các hàm Map Config
        // Config chỉ có thể sửa không thêm mới, không xóa, nên chỉ thực thi 2 phương thức MapEntityToDTO và MapUpdateDTOToEntity
        public override Config MapCreateDTOToEntity(ConfigCreateDTO createDTO)
        {
            throw new NotImplementedException();
        }

        public override Config MapDTOToEntity(ConfigDTO dto)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Hàm chuyển đổi Config sang ConfigDTO
        /// </summary>
        /// <param name="entity">Thông tin Config</param>
        /// <returns>Thông tin ConfigDTO</returns>
        public override ConfigDTO MapEntityToDTO(Config entity)
        {
            var config = _mapper.Map<ConfigDTO>(entity);
            return config;
        }

        public override Config MapUpdateDTOToEntity(ConfigUpdateDTO updateDTO, Config entity)
        {
            var config = _mapper.Map(updateDTO, entity);
            return config;
        }
        #endregion

    }
}
