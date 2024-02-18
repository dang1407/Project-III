using WebAPI.Application;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure
{
    public class DateConfigurationRepository : BaseRepository<DateConfiguration>, IDateConfigurationRepository
    {
        public DateConfigurationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
