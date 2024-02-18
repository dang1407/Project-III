using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class ParkMemberService : BaseService<ParkMember, ParkMemberDTO, ParkMemberCreateDTO, ParkMemberUpdateDTO>, IParkMemberService
    {
        private readonly IMapper _mapper;   
        private readonly IParkMemberRepository _parkMemberRepository;
        public ParkMemberService(IParkMemberRepository parkMemberRepository, IMapper mapper) : base(parkMemberRepository)
        {
            _mapper = mapper;
            _parkMemberRepository = parkMemberRepository;   
        }

        public Task<byte[]> ExportParkMemberExcelAsync(List<ParkMemberDTO> parkMemberDTOs, int page, int pageSize)
        {
            var parkMembers = parkMemberDTOs.Select(parkMemberDTO => MapDTOToEntity(parkMemberDTO)).ToList();   

            var excelBytes = _parkMemberRepository.ExportParkMembersExcelAsync(parkMembers, page, pageSize);    

            return excelBytes;  
        }

        public async Task<List<ParkMemberDTO>> GetByListIdAsync(List<Guid> ids)
        {
            var parkMembers = await _parkMemberRepository.GetByListIdAsync(ids);
            var parkMemberDTOs = parkMembers.Select(parkMember => MapEntityToDTO(parkMember)).ToList(); 
            return parkMemberDTOs;  
        }

        /// <summary>
        /// Hàm lấy ra mã khách hàng gửi xe mới
        /// </summary>
        /// <returns>Mã khách hàng gửi xe mới (string)</returns>
        public async Task<string> GetNewParkMemberCodeAsync()
        {
            var newParkMemberCode = await _parkMemberRepository.GetNewParkMemberCodeAsync();
            return newParkMemberCode.ToString();
        }

        /// <summary>
        /// Hàm lấy ra số khách hàng gửi xe có trong database
        /// </summary>
        /// <returns>Số khách hàng gửi xe trong database (int)</returns>
        public async Task<int> GetNumParkMembersAsync()
        {
            int numParkMembers = await _parkMemberRepository.GetNumParkMembersAsync();  
            return numParkMembers;  
        }

        public async Task<List<ParkMemberDTO>?> GetParkMemberByFullNameAsync(int page, int pageSize, string parkMemberFullName)
        {
            // Số thứ trang trong DB bắt đầu từ 0 nhưng số thứ tự ở Frontend bắt đầu từ 1 nên cần trừ 1
            var parkMembers = await _parkMemberRepository.GetParkMemberByFullNameAsync(page - 1, pageSize, parkMemberFullName); 
            if(parkMembers != null)
            {
                var parkMemberDTOs = parkMembers.Select(parkMember => MapEntityToDTO(parkMember)).ToList();
                return parkMemberDTOs;
            }
            else return null;  
              
        }

        /// <summary>
        /// Hàm tìm kiếm khách hàng gửi xe theo số điện thoại
        /// </summary>
        /// <param name="mobile">Số điện thoại khách hàng</param>
        /// <returns>Thông tin khách hàng nếu tìm thấy</returns>
        /// Created by: nkmdang 1/1/2024
        public async Task<ParkMemberDTO?> GetParkMemberByMobileAsync(string mobile)
        {
            var parkMember = await _parkMemberRepository.GetParkMemberByMobileAsync(mobile);
            if(parkMember != null)
            {
                var parkMemberDTO = MapEntityToDTO(parkMember);
                return parkMemberDTO;
            }
            else
            {
                return null;    
            }
        }

        /// <summary>
        /// Hàm tìm kiếm khách hàng gửi xe theo mã khách hàng
        /// </summary>
        /// <param name="parkMemberCode">Mã khách hàng</param>
        /// <returns>Thông tin khách hàng tìm kiếm theo mã khách hàng</returns>
        /// Created by: nkmdang 1/1/2024
        public async Task<ParkMemberDTO?> GetParkMemberByParkMemberCodeAsync(string parkMemberCode)
        {
            var parkMember = await _parkMemberRepository.GetParkMemberByParkMemberCodeAsync(parkMemberCode);
            if (parkMember != null)
            {
                var parkMemberDTO = MapEntityToDTO(parkMember);
                return parkMemberDTO;
            }
            else
            { 
                return null; 
            }
        }
        /// <summary>
        /// Hàm tìm kiếm khách hàng gửi xe theo biển số xe
        /// </summary>
        /// <param name="licensePlate">Biển số xe cần tìm kiếm</param>
        /// <returns></returns>
        /// Created by: nkmdang 17/1/2023
        public async Task<ParkMemberDTO?> GetParkMemberByLicensePlateAsync(string licensePlate)
        {
            var result = await _parkMemberRepository.GetParkMemberByLicensePlateAsync(licensePlate);
            return MapEntityToDTO(result);   
        }

        /// <summary>
        /// Hàm lấy thông tin khách hàng theo phân trang 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<ParkMemberDTO>> GetParkMembersByPaginationAsync(int page, int pageSize)
        {
            var parkMembers = await _parkMemberRepository.GetParkMembersPaginationAsync(page - 1, pageSize); 
            var parkMemberDTOs = parkMembers.Select(parkMember => MapEntityToDTO(parkMember)).ToList();
            return parkMemberDTOs;
        }


        /// <summary>
        /// Hàm chuyển đổi từ ParkMemberCreateDTO sang ParkMember
        /// </summary>
        /// <param name="createDTO">ParkMemberCreate </param>
        /// <returns>ParkMember</returns>
        /// Created by: nkmdang (21/09/2023)
        public override ParkMember MapCreateDTOToEntity(ParkMemberCreateDTO createDTO)
        {
            var parkMember = _mapper.Map<ParkMember>(createDTO);
            // Kiểm tra ParkMemberId chưa có 
            if (parkMember.ParkMemberId == Guid.Empty)
            {
                parkMember.ParkMemberId = Guid.NewGuid();
            }
            return parkMember;
        }


        /// <summary>
        /// Hàm chuyển đổi ParkMemberUpdateDTO sang ParkMember
        /// </summary>
        /// <param name="updateDTO">Đối tượng UpdateParkMemberDTO nhận vào body</param>
        /// <param name="entity"></param> Đối tượng ParkMember đầu đủ thông tin 
        /// <returns>ParkMember</returns>
        /// Created by: nkmdang (21/09/2023)
        public override ParkMember MapUpdateDTOToEntity(ParkMemberUpdateDTO updateDTO, ParkMember entity)
        {
            var parkMember = _mapper.Map(updateDTO, entity);
            return parkMember;
        }


        /// <summary>
        /// Hàm chuyển đổi từ ParkMember sang DTO
        /// </summary>
        /// <param name="parkMember">Thông tin nhân viên</param>
        /// <returns></returns>
        /// Created by: nkmdang (21/09/2023)
        public override ParkMemberDTO MapEntityToDTO(ParkMember parkMember)
        {
            var parkMemberDTO = _mapper.Map<ParkMemberDTO>(parkMember);
            return parkMemberDTO;
        }

        /// <summary>
        /// Hàm map ParkMemberDTO to ParkMember
        /// </summary>
        /// <param name="dto">ParkMemberDTO</param>
        /// <returns>Object ParkMember</returns>
        /// Created by: nkmdang 2/1/2024
        public override ParkMember MapDTOToEntity(ParkMemberDTO dto)
        {
            var parkMember = _mapper.Map<ParkMember>(dto);  
            return parkMember;  
        }

        
    }
}
