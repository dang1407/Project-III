using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;
using WebAPI.Application;

namespace WebAPI.Controllers
{

    public class DepartmentsController : BaseReadOnlyController<DepartmentDTO>
    {
        private readonly IDepartmentService _departmentsService;    
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentsService = departmentService;
        }



        /// <summary>
        /// Hàm lấy ra Department theo tên
        /// </summary>
        /// <param name="departmentName">Tên của Department (string)</param>
        /// <returns>Thông tin department</returns>
        /// Created by: nkmdang (21/09/2023)
        [HttpGet]
        [Route("DepartmentName")]
        public async Task<DepartmentDTO> GetDepartmentByNameAsync(string departmentName)
        {
            var result = await _departmentsService.GetDepartmentByNameAsync(departmentName);
            return result;
        }


        /// <summary>
        /// Hàm lấy ra tất cả thông tin về đơn vị
        /// </summary>
        /// <returns>Thông tin đơn vị</returns>
        /// Created by: nkmdang (28/09/2023)
        [HttpGet]
        [Route("")]
        public async Task<List<DepartmentDTO>> GetAllDepartment()
        {
            var result = await _departmentsService.GetFilterAsync(1, 100, null);   
            return result;
        }
    }
}
