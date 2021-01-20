using Core.Utilities.Results;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAccountIdentityService
    {
        IDataResult<AccountIdentityDTO> GetByEmail(string email);

        IDataResult<AccountIdentityDTO> GetByEmailAndPassword(AccountIdentityDTO input,string passwordToCheck);
    }
}
