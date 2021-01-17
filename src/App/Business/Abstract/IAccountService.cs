using Core.Utilities.Results;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAccountService
    {
        IDataResult<AccountDTO> Get(int id);
    }
}
