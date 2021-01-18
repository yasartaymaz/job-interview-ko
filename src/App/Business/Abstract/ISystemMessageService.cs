using Core.Utilities.Results;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ISystemMessageService
    {
        IDataResult<SystemMessageDTO> Get(int id);
    }
}
