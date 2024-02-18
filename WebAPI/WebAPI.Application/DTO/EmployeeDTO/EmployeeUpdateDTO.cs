using WebAPI.Domain;
using WebAPI.Domain.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class EmployeeUpdateDTO : BaseDTO
    {
        #region Properties
        public Guid EmployeeId { get; set; }


        [Required(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmployeeCodeNotEmpty")]
        [RegularExpression(@"^NV-00[0-9]{4}$", ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmployeeCodeNotValid")]
        public string EmployeeCode { get; set; }


        [Required(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "FullNameNotEmpty")]
        [StringLength(100, ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "FullNameNotEmpty")]
        public string FullName { get; set; }


        [DateNotInFuture(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DateOfBirthNotInFuture")]
        [DateOfBirthValidate(18, 70, ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DateOfBirthNotValid")]
        public DateTimeOffset? DateOfBirth { get; set; }



        public Gender? Gender { get; set; }


        public Guid? DepartmentId { get; set; }

        [Required(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DepartmentNameNotEmpty")]
        public string DepartmentName { get; set; }


        [Phone(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "LandLinePhoneNotValid")]
        public string? LandLinePhone { get; set; }

        [Phone(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "MobileNotValid")]
        public string? Mobile { get; set; }

        public string? PositionName { get; set; }

        public string? Address { get; set; }

        public string? BankAccount { get; set; }
        public string? BankBranch { get; set; }

        public string? BankName { get; set; }

        public string? PersonalIdentification { get; set; }

        [DateNotInFuture(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "PICreatedDateNotInFuture")]
        public DateTimeOffset? PICreatedDate { get; set; }

        public string? PICreatedPlace { get; set; }

        [RegularExpression(@"^.+@gmail\.com$", ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmailNotValid")]
        public string? Email { get; set; }
        #endregion
       

        
    }
}
