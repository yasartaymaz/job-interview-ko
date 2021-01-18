using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class SystemMessageManager : ISystemMessageService
    {
        private readonly ISystemMessageDA _systemMessageDA;
        private readonly IMapper _mapper;

        public SystemMessageManager(ISystemMessageDA systemMessageDA, IMapper mapper)
        {
            _systemMessageDA = systemMessageDA;
            _mapper = mapper;
        }

        public IDataResult<SystemMessageDTO> Get(int id)
        {
            SystemMessageDTO result = _systemMessageDA.GetWithType(id);

            return new SuccessDataResult<SystemMessageDTO>(result);
        }
    }
}
