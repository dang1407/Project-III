using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;
using WebAPI.Application.DTO;

namespace WebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class LeaveDaysRequestsController : ControllerBase
    //{
    //    private readonly ILeaveDaysRequestService _leaveDaysRequestService;
    //    private readonly IEmployeeService _employeeService;
    //    public LeaveDaysRequestsController(ILeaveDaysRequestService leaveDaysRequestService, IEmployeeService employeeService) 
    //    {
    //        _leaveDaysRequestService = leaveDaysRequestService;
    //        _employeeService = employeeService; 
    //    }    

    //    [HttpGet]
    //    [Route("")]
    //    public async Task<List<LeaveDaysRequestDTO>> GetAllAsync()
    //    {
    //        var rawLeaveDaysRequest = await _leaveDaysRequestService.GetAllAsync();
    //        return rawLeaveDaysRequest;
    //    }
    //}

    public class LeaveDaysRequestsController : BaseController<LeaveDaysRequestDTO, LeaveDaysRequestCreateDTO, LeaveDaysRequestUpdateDTO>
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILeaveDaysRequestService _leaveDaysRequestService; 
        public LeaveDaysRequestsController(ILeaveDaysRequestService leaveDaysRequestService, IEmployeeService employeeService) : base(leaveDaysRequestService) 
        {
            _employeeService = employeeService;
            _leaveDaysRequestService = leaveDaysRequestService;
        }

        [HttpGet]
        [Route("{ldrid}")]
        public async Task<Object> GetOneLeaveDaysRequestAndDetailAsync(Guid id)
        {
            var leaveDaysRequest = await base.GetByIdAsync(id);
            string[] leaveEmployeesIdString = leaveDaysRequest.LeaveEmployeesId.Split(',');
            string[] relateEmployeesIdString = leaveDaysRequest.RelateEmployeesId.Split(','); 
            List<Guid> rawLeaveEmployeesId = leaveEmployeesIdString.Select(i => new Guid(i)).ToList();
            List<Guid> rawRelateEmployeeId = relateEmployeesIdString.Select(i => new Guid(i)).ToList();    
            List<Guid> cleanLeaveEmployeesId = new List<Guid>();
            List<Guid> cleanRelateEmployeesId = new List<Guid>();
            foreach(Guid employeeId in rawLeaveEmployeesId) 
            {
                if(employeeId != Guid.Empty)
                {
                    cleanLeaveEmployeesId.Add(employeeId);
                }
            }

            foreach (Guid employeeId in rawRelateEmployeeId)
            {
                if(employeeId != Guid.Empty) 
                {
                    cleanRelateEmployeesId.Add(employeeId);   
                }
            }
            var leaveEmployees = await _employeeService.GetByListIdAsync(cleanLeaveEmployeesId);
            var relatedEmployees = await _employeeService.GetByListIdAsync(cleanRelateEmployeesId);
            return new
            {
                leaveDaysRequest = leaveDaysRequest,    
                leaveEmployees = leaveEmployees,    
                relatedEmployees = relatedEmployees,    
            };
        }

        [HttpGet]
        [Route("")]
        public async Task<dynamic> GetLeaveDaysRequestsAsync(int page, int pageSize)
        {
            var result = await _leaveDaysRequestService.GetLeaveDaysRequestsAsync(page, pageSize);
            var numLeaveDaysRequest = await _leaveDaysRequestService.GetNumLeaveDaysRequestAsync();
            return new 
            {
                data = result,  
                numLeaveDaysRequest = numLeaveDaysRequest,
            };  
        }
    }
}
