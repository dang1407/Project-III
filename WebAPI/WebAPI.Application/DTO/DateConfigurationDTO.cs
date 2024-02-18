using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class DateConfigurationDTO : BaseDTO
    {
        /// <summary>
        /// Định danh của DateConfiguration
        /// </summary>
        /// Created by: nkmdang (07/10/2023)
        public Guid DateConfigurationId { get; set; }

        /// <summary>
        /// Chuỗi biểu diễn định dạng DateConfiguration
        /// </summary>
        /// Created by: nkmdang (07/10/2023)
        public string DatePattern { get; set; }
    }
}
