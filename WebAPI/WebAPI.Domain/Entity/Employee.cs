using WebAPI.Domain.Resource;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;


namespace WebAPI.Domain
{
    public class Employee : BaseEntity, IEntity
    {

        #region Properties
        public Guid EmployeeId { get; set; }


        [Required(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmployeeCodeNotEmpty")]
        [RegularExpression(@"^NV-00[0-9]{4}$", ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "EmployeeCodeNotValid")]
        public string EmployeeCode { get; set; }


        [Required(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "FullNameNotEmpty")]
        [StringLength(100, ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "FullNameNotEmpty")]
        public string FullName { get; set; }

        public Guid DepartmentId { get; set; }
        public Gender? Gender { get; set; }

        [DataType(DataType.DateTime)]
        [DateNotInFuture(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DateOfBirthNotInFuture")]
        [DateOfBirthValidate(18, 70, ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DateOfBirthNotValid")]
        public DateTimeOffset? DateOfBirth { get; set; }


        public string? Address { get; set; }




        [Required(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DepartmentNameNotEmpty")]
        [StringLength(255, ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "DepartmentNameNotTooLong")]
        public string DepartmentName { get; set; }


        [Phone(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "LandLinePhoneNotValid")]
        public string? LandLinePhone { get; set; }

        [Phone(ErrorMessageResourceType = typeof(EmployeeResource), ErrorMessageResourceName = "MobileNotValid")]
        public string? Mobile { get; set; }

        public string? PositionName { get; set; }


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

        #region Override method ToString()
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion

        #region Implement IEntity

        /// <summary>
        /// Hàm lấy ra Id của Employee
        /// </summary>
        /// <returns>Id của Employee (Guid)</returns>
        /// Created by: nkmdang (19/09/2023)
        public Guid GetId()
        {
            return EmployeeId;
        }


        /// <summary>
        /// Hàm gán giá trị cho Id của Employee
        /// </summary>
        /// <param name="id">Id cần gán cho Employee (Guid)</param>
        /// Created by: nkmdang (19/09/2023)
        public void SetId(Guid id)
        {
            EmployeeId = id;
        }

        #endregion
    }
}
