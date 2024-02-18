using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{

    /// <summary>
    /// Lớp biểu diễn cho các định dạng ngày tháng năm
    /// </summary>
    /// Created by: nkmdang (07/10/2023)
    public class DateConfiguration : BaseEntity, IEntity
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

        /// <summary>
        /// Hàm lấy ra Id của DateConfiguration
        /// </summary>
        /// <returns>Id của DateConfiguration</returns>
        /// Created by: nkmdang (07/10/2023)
        public Guid GetId()
        {
            return DateConfigurationId;   
        }

        public void SetId(Guid id)
        {

            DateConfigurationId = id;  
        }
    }
}
