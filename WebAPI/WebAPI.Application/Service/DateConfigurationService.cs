using AutoMapper;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public class DateConfigurationService : BaseReadOnlyService<DateConfiguration, DateConfigurationDTO>,IDateConfigurationService
    {
        private readonly IDateConfigurationRepository _dateFormatRepository;
        private readonly IMapper _mapper;
        public DateConfigurationService(IDateConfigurationRepository dateFormatRepository, IMapper mapper) : base(dateFormatRepository) 
        {
            _dateFormatRepository = dateFormatRepository;
            _mapper = mapper;
        }
        public override DateConfigurationDTO MapEntityToDTO(DateConfiguration entity)
        {
            var dateFormatDTO = _mapper.Map<DateConfigurationDTO>(entity); 
            return dateFormatDTO;
        }
    }
}
