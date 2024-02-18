using AutoMapper;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class DepartmentService : BaseReadOnlyService<Department, DepartmentDTO>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentValidate _departmentValidate;
        private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository departmentRepository, IDepartmentValidate departmentValidate, IMapper mapper) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;   
            _departmentValidate = departmentValidate;
            _mapper = mapper;
        }

        public override DepartmentDTO MapEntityToDTO(Department entity)
        {
            var departmentDTO = _mapper.Map<DepartmentDTO>(entity);    
            return departmentDTO;
        }


        /// <summary>
        /// Hàm lấy thông tin đơn vị qua tên đơn vị
        /// </summary>
        /// <param name="departmentName">Tên đơn vị</param>
        /// <returns>Thông tin đơn vị</returns>
        public async Task<DepartmentDTO> GetDepartmentByNameAsync(string departmentName)
        {
            var result = await _departmentRepository.GetDepartmentByNameAsync(departmentName);
            if (result == null)
            {
                throw new NotFoundException(DepartmentUserMessageResource.DepartmentNotFound, DepartmentUserMessageResource.DepartmentNotFound, 404);
            }
            return MapEntityToDTO(result);  
        }


        
    }
}
