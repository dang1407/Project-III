using AutoMapper;
using WebAPI.Application.DTO;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class LeaveDaysRequestService : BaseService<LeaveDaysRequest, LeaveDaysRequestDTO, LeaveDaysRequestCreateDTO, LeaveDaysRequestUpdateDTO>, ILeaveDaysRequestService
    {
        private readonly IMapper _mapper;   
        private ILeaveDaysRequestRepository _leaveDaysRequestRepository;
        public LeaveDaysRequestService(ILeaveDaysRequestRepository leaveDaysRequestRepository, IMapper mapper): base(leaveDaysRequestRepository) 
        {
            _mapper = mapper;
            _leaveDaysRequestRepository = leaveDaysRequestRepository;
        }

        /// <summary>
        /// Hàm lấy ra thông tin đơn nghỉ theo phân trang
        /// </summary>
        /// <param name="page">Số thứ tự trang</param>
        /// <param name="pageSize">Số bản ghi trong trang</param>
        /// <returns>Thông tin các đơn nghỉ</returns>
        /// Created By: nkdang 31/10/2023
        public async Task<List<LeaveDaysRequestDTO>> GetLeaveDaysRequestsAsync(int page, int pageSize)
        {
            var result = await _leaveDaysRequestRepository.GetLeaveDaysRequestsAsync(page, pageSize);
            var leaveDaysRequestDTOs = result.Select(leaveDaysRequest => MapEntityToDTO(leaveDaysRequest)).ToList();
            return leaveDaysRequestDTOs;
        }

        /// <summary>
        /// Hàm lấy ra số đơn xin nghỉ hiện có trong database
        /// </summary>
        /// <returns>Số đơn xin nghỉ hiện có trong database</returns>
        /// Created by: nkdang 1/11/2023
        public async Task<int> GetNumLeaveDaysRequestAsync()
        {
            var result = await _leaveDaysRequestRepository.GetNumLeaveDaysRequestAsync();
            return result;
        }

        //public async Task<List<LeaveDaysRequestDTO>> GetAllAsync()
        //{
        //    var rawData = await base.GetAllAsync();
        //    return rawData;
        //}

        /// <summary>
        /// Hàm Map từ LeaveDaysRequestCreateDTO sang LeaveDaysRequest
        /// </summary>
        /// <param name="createDTO">Thông tin để tạo mới đơn nghỉ</param>
        /// <returns>Thông tin entity của đơn nghỉ</returns>
        /// Created By: nkdang 31/10/2023
        public override LeaveDaysRequest MapCreateDTOToEntity(LeaveDaysRequestCreateDTO createDTO)
        {
            var leaveDaysRequest = _mapper.Map<LeaveDaysRequest>(createDTO);
            return leaveDaysRequest;
        }


        /// <summary>
        /// Hàm map từ LeaveDaysRequestDTO sang LeaveDaysRequest
        /// </summary>
        /// <param name="dto">Thông tin DTO của Đơn nghỉ</param>
        /// <returns>LeaveDaysRequest</returns>
        /// Created By: nkdang 31/10/2023
        public override LeaveDaysRequest MapDTOToEntity(LeaveDaysRequestDTO dto)
        {
            var leaveDaysRequest = _mapper.Map<LeaveDaysRequest>(dto);
            return leaveDaysRequest;    
        }

        /// <summary>
        /// Hàm Map từ LeaveDaysRequest sang LeaveDaysRequestDTO
        /// </summary>
        /// <param name="entity">Entity LeaveDaysRequest</param>
        /// <returns></returns>
        /// Created By: nkdang 31/10/2023
        public override LeaveDaysRequestDTO MapEntityToDTO(LeaveDaysRequest entity)
        {
            var leaveDaysRequestDTO = _mapper.Map<LeaveDaysRequestDTO>(entity);
            return leaveDaysRequestDTO;
        }


        /// <summary>
        /// Hàm Map từ LeaveDaysRequestUpdateDTO sang LeaveDaysRequest
        /// </summary>
        /// <param name="updateDTO">Thông tin đơn nghỉ sửa đổi</param>
        /// <param name="entity">Thông tin đơn nghỉ sẽ cập nhật vào DB</param>
        /// <returns>Thông tin đơn nghỉ sẽ cập nhật vào DB</returns>
        /// Created By: nkdang 31/10/2023
        public override LeaveDaysRequest MapUpdateDTOToEntity(LeaveDaysRequestUpdateDTO updateDTO, LeaveDaysRequest entity)
        {
            var leaveDaysRequest = _mapper.Map(updateDTO, entity);
            return leaveDaysRequest;
        }
    }
}
